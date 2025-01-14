using MagicOnion.Generator.CodeAnalysis;
using MagicOnion.Generator.CodeGen.Extensions;
using MagicOnion.Generator.Internal;
using System.CodeDom.Compiler;

namespace MagicOnion.Generator.CodeGen;

public class StaticStreamingHubClientGenerator
{
    class StreamingHubClientBuildContext
    {
        public StreamingHubClientBuildContext(MagicOnionStreamingHubInfo hub, IndentedTextWriter textWriter)
        {
            Hub = hub;
            TextWriter = textWriter;
        }

        public MagicOnionStreamingHubInfo Hub { get; }

        public IndentedTextWriter TextWriter { get; }
    }

    public static string Build(IEnumerable<MagicOnionStreamingHubInfo> hubs)
    {
        var baseWriter = new StringWriter();
        var textWriter = new IndentedTextWriter(baseWriter);

        EmitHeader(textWriter);

        foreach (var hubInfo in hubs)
        {
            var buildContext = new StreamingHubClientBuildContext(hubInfo, textWriter);

            using (textWriter.IfDirective(hubInfo.IfDirectiveCondition)) // #if ...
            {
                EmitPreamble(buildContext);
                EmitHubClientClass(buildContext);
                EmitPostscript(buildContext);
            } // #endif
        }

        return baseWriter.ToString();
    }

    static void EmitHeader(IndentedTextWriter textWriter)
    {
        textWriter.WriteLines("""
        #pragma warning disable 618
        #pragma warning disable 612
        #pragma warning disable 414
        #pragma warning disable 219
        #pragma warning disable 168
        
        // NOTE: Disable warnings for nullable reference types.
        // `#nullable disable` causes compile error on old C# compilers (-7.3)
        #pragma warning disable 8603 // Possible null reference return.
        #pragma warning disable 8618 // Non-nullable variable must contain a non-null value when exiting constructor. Consider declaring it as nullable.
        #pragma warning disable 8625 // Cannot convert null literal to non-nullable reference type.
        """);
        textWriter.WriteLine();
    }

    static void EmitPreamble(StreamingHubClientBuildContext ctx)
    {
        ctx.TextWriter.WriteLines($$"""
        namespace {{ctx.Hub.ServiceType.Namespace}}
        {
            using global::System;
            using global::Grpc.Core;
            using global::MagicOnion;
            using global::MagicOnion.Client;
            using global::MessagePack;
        """);
        ctx.TextWriter.Indent++;
        ctx.TextWriter.WriteLine();
    }

    static void EmitPostscript(StreamingHubClientBuildContext ctx)
    {
        ctx.TextWriter.Indent--;
        ctx.TextWriter.WriteLine("}");
        ctx.TextWriter.WriteLine();
    }

    static void EmitHubClientClass(StreamingHubClientBuildContext ctx)
    {
        ctx.TextWriter.WriteLines($$"""
        [global::MagicOnion.Ignore]
        public class {{ctx.Hub.GetClientName()}} : global::MagicOnion.Client.StreamingHubClientBase<{{ctx.Hub.ServiceType.FullName}}, {{ctx.Hub.Receiver.ReceiverType.FullName}}>, {{ctx.Hub.ServiceType.FullName}}
        {
        """);
        using (ctx.TextWriter.BeginIndent())
        {
            EmitProperties(ctx);
            EmitConstructor(ctx);
            EmitHubMethods(ctx, isFireAndForget: false);
            EmitFireAndForget(ctx);
            EmitOnBroadcastEvent(ctx);
            EmitOnResponseEvent(ctx);
        }
        ctx.TextWriter.WriteLine("}");
        // }
    }

    static void EmitProperties(StreamingHubClientBuildContext ctx)
    {
        ctx.TextWriter.WriteLine("protected override global::Grpc.Core.Method<global::System.Byte[], global::System.Byte[]> DuplexStreamingAsyncMethod { get; }");
        ctx.TextWriter.WriteLine();
    }
    
    static void EmitConstructor(StreamingHubClientBuildContext ctx)
    {
        ctx.TextWriter.WriteLines($$"""
        public {{ctx.Hub.GetClientName()}}(global::Grpc.Core.CallInvoker callInvoker, global::System.String host, global::Grpc.Core.CallOptions options, global::MagicOnion.Serialization.IMagicOnionSerializerProvider serializerProvider, global::MagicOnion.Client.IMagicOnionClientLogger logger)
            : base(callInvoker, host, options, serializerProvider, logger)
        {
            var marshaller = global::MagicOnion.MagicOnionMarshallers.ThroughMarshaller;
            DuplexStreamingAsyncMethod = new global::Grpc.Core.Method<global::System.Byte[], global::System.Byte[]>(global::Grpc.Core.MethodType.DuplexStreaming, "{{ctx.Hub.ServiceType.Name}}", "Connect", marshaller, marshaller);
        }
        """);
        ctx.TextWriter.WriteLine();
    }

    static void EmitFireAndForget(StreamingHubClientBuildContext ctx)
    {
        ctx.TextWriter.WriteLines($$"""
        public {{ctx.Hub.ServiceType.FullName}} FireAndForget()
            => new FireAndForgetClient(this);
            
        [global::MagicOnion.Ignore]
        class FireAndForgetClient : {{ctx.Hub.ServiceType.FullName}}
        {
            readonly {{ctx.Hub.GetClientName()}} parent;

            public FireAndForgetClient({{ctx.Hub.GetClientName()}} parent)
                => this.parent = parent;

            public {{ctx.Hub.ServiceType.FullName}} FireAndForget() => this;
            public global::System.Threading.Tasks.Task DisposeAsync() => throw new global::System.NotSupportedException();
            public global::System.Threading.Tasks.Task WaitForDisconnect() => throw new global::System.NotSupportedException();

        """);
        using (ctx.TextWriter.BeginIndent())
        {
            EmitHubMethods(ctx, isFireAndForget: true);
        }
        ctx.TextWriter.WriteLine("}");
        ctx.TextWriter.WriteLine();
    }

    static void EmitHubMethods(StreamingHubClientBuildContext ctx, bool isFireAndForget)
    {
        // public Task MethodReturnWithoutValue()
        //     => WriteMessageWithResponseAsync<MessagePack.Nil, MessagePack.Nil>(FNV1A32.GetHashCode(nameof(MethodReturnWithoutValue)), MessagePack.Nil.Default);
        // public Task<int> MethodParameterless()
        //     => WriteMessageWithResponseAsync<MessagePack.Nil, int>(FNV1A32.GetHashCode(nameof(MethodParameterless)), MessagePack.Nil.Default);
        // public Task<int> MethodParameter_One(int arg0)
        //     => WriteMessageWithResponseAsync<int, int>(FNV1A32.GetHashCode(nameof(MethodParameter_One)), arg0);
        // public Task<int> MethodParameter_Many(int arg0, string arg1)
        //     => WriteMessageWithResponseAsync<DynamicArgumentTuple<int, string>, int>(FNV1A32.GetHashCode(nameof(MethodParameter_Many)), new DynamicArgumentTuple<int, string>(arg0, arg1));
        foreach (var method in ctx.Hub.Methods)
        {
            using (ctx.TextWriter.IfDirective(method.IfDirectiveCondition)) // #if ...
            {
                var writeMessageParameters = method.Parameters.Count switch
                {
                    // Nil.Default
                    0 => $", global::MessagePack.Nil.Default",
                    // arg0
                    1 => $", {method.Parameters[0].Name}",
                    // new DynamicArgumentTuple(arg1, arg2, ...)
                    _ => $", {method.Parameters.ToNewDynamicArgumentTuple()}",
                };

                if (method.MethodReturnType == MagicOnionTypeInfo.KnownTypes.System_Threading_Tasks_ValueTask)
                {
                    // ValueTask
                    ctx.TextWriter.WriteLines($"""
                    public {method.MethodReturnType.FullName} {method.MethodName}({method.Parameters.ToMethodSignaturize()})
                        => new global::System.Threading.Tasks.ValueTask({(isFireAndForget ? "parent.WriteMessageFireAndForgetAsync" : "base.WriteMessageWithResponseAsync")}<{method.RequestType.FullName}, {method.ResponseType.FullName}>({method.HubId}{writeMessageParameters}));
                    """);
                }
                else if (method.MethodReturnType.HasGenericArguments && method.MethodReturnType.GetGenericTypeDefinition() == MagicOnionTypeInfo.KnownTypes.System_Threading_Tasks_ValueTask)
                {
                    // ValueTask<T>
                    ctx.TextWriter.WriteLines($"""
                    public {method.MethodReturnType.FullName} {method.MethodName}({method.Parameters.ToMethodSignaturize()})
                        => new global::System.Threading.Tasks.ValueTask<{method.ResponseType.FullName}>({(isFireAndForget ? "parent.WriteMessageFireAndForgetAsync" : "base.WriteMessageWithResponseAsync")}<{method.RequestType.FullName}, {method.ResponseType.FullName}>({method.HubId}{writeMessageParameters}));
                    """);
                }
                else
                {
                    // Task, Task<T>
                    ctx.TextWriter.WriteLines($"""
                    public {method.MethodReturnType.FullName} {method.MethodName}({method.Parameters.ToMethodSignaturize()})
                        => {(isFireAndForget ? "parent.WriteMessageFireAndForgetAsync" : "base.WriteMessageWithResponseAsync")}<{method.RequestType.FullName}, {method.ResponseType.FullName}>({method.HubId}{writeMessageParameters});
                    """);
                }
            } // #endif
        }

        ctx.TextWriter.WriteLine();
    }

    static void EmitOnBroadcastEvent(StreamingHubClientBuildContext ctx)
    {
        ctx.TextWriter.WriteLine("protected override void OnBroadcastEvent(global::System.Int32 methodId, global::System.ArraySegment<global::System.Byte> data)");
        ctx.TextWriter.WriteLine("{");
        using (ctx.TextWriter.BeginIndent())
        {
            ctx.TextWriter.WriteLine("switch (methodId)");
            ctx.TextWriter.WriteLine("{");
            using (ctx.TextWriter.BeginIndent())
            {
                foreach (var method in ctx.Hub.Receiver.Methods)
                {
                    using (ctx.TextWriter.IfDirective(method.IfDirectiveCondition))
                    {
                        var methodArgs = method.Parameters.Count switch
                        {
                            0 => "",
                            1 => "value",
                            _ => string.Join(", ", Enumerable.Range(1, method.Parameters.Count).Select(x => $"value.Item{x}"))
                        };

                        ctx.TextWriter.WriteLines($$"""
                        case {{method.HubId}}: // {{method.MethodReturnType.ToDisplayName()}} {{method.MethodName}}({{method.Parameters.ToMethodSignaturize()}})
                            {
                                var value = base.Deserialize<{{method.RequestType.FullName}}>(data);
                                receiver.{{method.MethodName}}({{methodArgs}});
                            }
                            break;
                        """);
                    }
                }
            }
            ctx.TextWriter.WriteLine("}");
        }
        ctx.TextWriter.WriteLine("}");
        ctx.TextWriter.WriteLine();
    }

    static void EmitOnResponseEvent(StreamingHubClientBuildContext ctx)
    {
        ctx.TextWriter.WriteLine("protected override void OnResponseEvent(global::System.Int32 methodId, global::System.Object taskCompletionSource, global::System.ArraySegment<global::System.Byte> data)");
        ctx.TextWriter.WriteLine("{");
        using (ctx.TextWriter.BeginIndent())
        {
            ctx.TextWriter.WriteLine("switch (methodId)");
            ctx.TextWriter.WriteLine("{");
            using (ctx.TextWriter.BeginIndent())
            {
                foreach (var method in ctx.Hub.Methods)
                {
                    using (ctx.TextWriter.IfDirective(method.IfDirectiveCondition))
                    {
                        ctx.TextWriter.WriteLines($$"""
                        case {{method.HubId}}: // {{method.MethodReturnType.ToDisplayName()}} {{method.MethodName}}({{method.Parameters.ToMethodSignaturize()}})
                            base.SetResultForResponse<{{method.ResponseType.FullName}}>(taskCompletionSource, data);
                            break;
                        """);
                    }
                }
            }
            ctx.TextWriter.WriteLine("}");
        }
        ctx.TextWriter.WriteLine("}");
        ctx.TextWriter.WriteLine();
    }
}

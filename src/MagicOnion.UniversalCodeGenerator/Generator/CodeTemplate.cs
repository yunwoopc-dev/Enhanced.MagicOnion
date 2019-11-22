﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace MagicOnion.Generator
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using MagicOnion.CodeAnalysis;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class CodeTemplate : CodeTemplateBase
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("#pragma warning disable 618\r\n#pragma warning disable 612\r\n#pragma warning disable" +
                    " 414\r\n#pragma warning disable 219\r\n#pragma warning disable 168\r\n\r\n");
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace != null ? ("namespace " + Namespace + " {") : ""));
            this.Write("\r\n    using System;\r\n    using MagicOnion;\r\n    using MagicOnion.Client;\r\n    usi" +
                    "ng Grpc.Core;\r\n    using MessagePack;\r\n");
 foreach(var interfaceDef in Interfaces) { 
            this.Write("\r\n");
 if(interfaceDef.IsIfDebug) { 
            this.Write("#if DEBUG\r\n");
 } 
 var clientName = interfaceDef.ClientName; 
            this.Write("    [Ignore]\r\n    public class ");
            this.Write(this.ToStringHelper.ToStringWithCulture(clientName));
            this.Write(" : MagicOnionClientBase<");
            this.Write(this.ToStringHelper.ToStringWithCulture(interfaceDef.FullName));
            this.Write(">, ");
            this.Write(this.ToStringHelper.ToStringWithCulture(interfaceDef.FullName));
            this.Write("\r\n    {\r\n");
 foreach(var item in interfaceDef.Methods) { 
 if(item.IsIfDebug) { 
            this.Write("#if DEBUG\r\n");
 } 
            this.Write("        static readonly Method<byte[], byte[]> ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            this.Write("Method;\r\n");
 if(item.MethodType == MethodType.Unary) { 
            this.Write("        static readonly Func<RequestContext, ResponseContext> ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            this.Write("Delegate;\r\n");
 } 
 if(item.IsIfDebug) { 
            this.Write("#endif\r\n");
 } 
 } 
            this.Write("\r\n        static ");
            this.Write(this.ToStringHelper.ToStringWithCulture(clientName));
            this.Write("()\r\n        {\r\n");
 foreach(var item in interfaceDef.Methods) { 
 if(item.IsIfDebug) { 
            this.Write("#if DEBUG\r\n");
 } 
            this.Write("            ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            this.Write("Method = new Method<byte[], byte[]>(MethodType.");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.MethodType.ToString()));
            this.Write(", \"");
            this.Write(this.ToStringHelper.ToStringWithCulture(interfaceDef.Name));
            this.Write("\", \"");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            this.Write("\", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshall" +
                    "er);\r\n");
 if(item.MethodType == MethodType.Unary) { 
            this.Write("            ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            this.Write("Delegate = _");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            this.Write(";\r\n");
 } 
 if(item.IsIfDebug) { 
            this.Write("#endif\r\n");
 } 
 } 
            this.Write("        }\r\n\r\n        ");
            this.Write(this.ToStringHelper.ToStringWithCulture(clientName));
            this.Write("()\r\n        {\r\n        }\r\n\r\n        public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(clientName));
            this.Write("(CallInvoker callInvoker, IFormatterResolver resolver, IClientFilter[] filters)\r\n" +
                    "            : base(callInvoker, resolver, filters)\r\n        {\r\n        }\r\n\r\n    " +
                    "    protected override MagicOnionClientBase<");
            this.Write(this.ToStringHelper.ToStringWithCulture(interfaceDef.Name));
            this.Write("> Clone()\r\n        {\r\n            var clone = new ");
            this.Write(this.ToStringHelper.ToStringWithCulture(clientName));
            this.Write(@"();
            clone.host = this.host;
            clone.option = this.option;
            clone.callInvoker = this.callInvoker;
            clone.resolver = this.resolver;
            clone.filters = filters;
            return clone;
        }

        public new ");
            this.Write(this.ToStringHelper.ToStringWithCulture(interfaceDef.Name));
            this.Write(" WithHeaders(Metadata headers)\r\n        {\r\n            return base.WithHeaders(he" +
                    "aders);\r\n        }\r\n\r\n        public new ");
            this.Write(this.ToStringHelper.ToStringWithCulture(interfaceDef.Name));
            this.Write(" WithCancellationToken(System.Threading.CancellationToken cancellationToken)\r\n   " +
                    "     {\r\n            return base.WithCancellationToken(cancellationToken);\r\n     " +
                    "   }\r\n\r\n        public new ");
            this.Write(this.ToStringHelper.ToStringWithCulture(interfaceDef.Name));
            this.Write(" WithDeadline(System.DateTime deadline)\r\n        {\r\n            return base.WithD" +
                    "eadline(deadline);\r\n        }\r\n\r\n        public new ");
            this.Write(this.ToStringHelper.ToStringWithCulture(interfaceDef.Name));
            this.Write(" WithHost(string host)\r\n        {\r\n            return base.WithHost(host);\r\n     " +
                    "   }\r\n\r\n        public new ");
            this.Write(this.ToStringHelper.ToStringWithCulture(interfaceDef.Name));
            this.Write(" WithOptions(CallOptions option)\r\n        {\r\n            return base.WithOptions(" +
                    "option);\r\n        }\r\n   \r\n");
 foreach(var item in interfaceDef.Methods) { 
 if(item.IsIfDebug) { 
            this.Write("#if DEBUG\r\n");
 } 
 if(item.MethodType == MethodType.Unary) { 
            this.Write("        static ResponseContext _");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            this.Write("(RequestContext __context)\r\n        {\r\n");
 if(item.Parameters.Length == 0) { 
            this.Write("            return CreateResponseContext<");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.ResponseType));
            this.Write(">(__context, ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            this.Write("Method);\r\n");
 } else { 
            this.Write("            return CreateResponseContext<");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.RequestMarshallerType));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.ResponseType));
            this.Write(">(__context, ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            this.Write("Method);\r\n");
 } 
            this.Write("        }\r\n");
 } 
            this.Write("\r\n        public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.ToString()));
            this.Write("\r\n        {\r\n");
 if(item.MethodType == MethodType.Unary) { 
 if(item.IsResponseTypeTaskOfT) { 
            this.Write("            return InvokeTaskAsync<");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.RequestMarshallerType));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.ResponseType));
            this.Write(">(\"");
            this.Write(this.ToStringHelper.ToStringWithCulture(interfaceDef.Name + "/" + item.Name));
            this.Write("\", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.UnaryRequestObject));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            this.Write("Delegate);\r\n");
 } else { 
            this.Write("            return InvokeAsync<");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.RequestMarshallerType));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.ResponseType));
            this.Write(">(\"");
            this.Write(this.ToStringHelper.ToStringWithCulture(interfaceDef.Name + "/" + item.Name));
            this.Write("\", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.UnaryRequestObject));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            this.Write("Delegate);\r\n");
 } 
 } else if(item.MethodType ==MethodType.ServerStreaming) { 
            this.Write("            var __request = ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.RequestObject()));
            this.Write(";\r\n            var __callResult = callInvoker.AsyncServerStreamingCall(");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            this.Write("Method, base.host, base.option, __request);\r\n            return System.Threading." +
                    "Tasks.Task.FromResult(new ServerStreamingResult<");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.ResponseType));
            this.Write(">(__callResult, base.resolver));\r\n");
 } else if(item.MethodType ==MethodType.ClientStreaming) { 
            this.Write("            var __callResult = callInvoker.AsyncClientStreamingCall<byte[], byte[" +
                    "]>(");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            this.Write("Method, base.host, base.option);\r\n            return System.Threading.Tasks.Task." +
                    "FromResult(new ClientStreamingResult<");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.RequestType));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.ResponseType));
            this.Write(">(__callResult, base.resolver));\r\n");
 } else if(item.MethodType ==MethodType.DuplexStreaming) { 
            this.Write("            var __callResult = callInvoker.AsyncDuplexStreamingCall<byte[], byte[" +
                    "]>(");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.Name));
            this.Write("Method, base.host, base.option);\r\n            return System.Threading.Tasks.Task." +
                    "FromResult(new DuplexStreamingResult<");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.RequestType));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(item.ResponseType));
            this.Write(">(__callResult, base.resolver));\r\n");
 } 
            this.Write("        }\r\n");
 if(item.IsIfDebug) { 
            this.Write("#endif\r\n");
 } 
 } 
            this.Write("    }\r\n");
 if(interfaceDef.IsIfDebug) { 
            this.Write("#endif \r\n");
 } 
 } 
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace != null ? "}" : ""));
            this.Write("\r\n\r\n#pragma warning restore 168\r\n#pragma warning restore 219\r\n#pragma warning res" +
                    "tore 414\r\n#pragma warning restore 612\r\n#pragma warning restore 618");
            return this.GenerationEnvironment.ToString();
        }
    }
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class CodeTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}

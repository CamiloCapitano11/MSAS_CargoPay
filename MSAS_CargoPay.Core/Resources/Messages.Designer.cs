﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MSAS_CargoPay.Core.Resources {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public Messages() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MSAS_CargoPay.Core.Resources.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a A bad request you have made..
        /// </summary>
        public static string BadRequest {
            get {
                return ResourceManager.GetString("BadRequest", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a Errors are the path to the dark side.  Errors lead to anger.   Anger leads to hate.  Hate leads to career change.  .
        /// </summary>
        public static string InternalServerError {
            get {
                return ResourceManager.GetString("InternalServerError", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a MSAS_CargoPay.
        /// </summary>
        public static string Microservice {
            get {
                return ResourceManager.GetString("Microservice", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a Resource Found it was not..
        /// </summary>
        public static string NotFound {
            get {
                return ResourceManager.GetString("NotFound", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a Recurso no encontrado..
        /// </summary>
        public static string RecursoNoEncontrado {
            get {
                return ResourceManager.GetString("RecursoNoEncontrado", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a Error.
        /// </summary>
        public static string StatusError {
            get {
                return ResourceManager.GetString("StatusError", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a Success.
        /// </summary>
        public static string StatusSuccess {
            get {
                return ResourceManager.GetString("StatusSuccess", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a Authorized you have not..
        /// </summary>
        public static string Unauthorized {
            get {
                return ResourceManager.GetString("Unauthorized", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a Some Thing Went Wrong..
        /// </summary>
        public static string Wrong {
            get {
                return ResourceManager.GetString("Wrong", resourceCulture);
            }
        }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NinjaRace {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Shaders {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Shaders() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NinjaRace.Shaders", typeof(Shaders).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to uniform sampler2D texture;
        ///uniform vec2 size;
        ///uniform float doX;
        /// 
        ///varying vec3 modelPos;
        /// 
        ///void main() {
        ///	const int n = 10;
        /// 
        ///	vec3 res1 = vec3(0, 0, 0);
        ///	float res2 = 0.0;
        ///	float w1 = 0.0, w2 = 0.0;
        /// 
        ///	vec4 res = vec4(0, 0, 0, 0);
        ///	float w = 0.0;
        /// 
        ///	float gb = 0.0;
        /// 
        ///	for(int i = -n; i &lt;= n; i++) {
        ///		vec2 pos = modelPos.xy + vec2(1.0 / size.x * float(i) * doX, 1.0 / size.y * float(i) * (1.0 - doX));
        ///		pos = clamp(pos, 0.0, 1.0);
        ///		float k = exp(-abs(float(i)) / float(n));
        ///		vec4 colo [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Glow {
            get {
                return ResourceManager.GetString("Glow", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to uniform sampler2D texture;
        ///uniform vec2 size;
        ///uniform vec4 color;
        ///
        ///varying vec3 modelPos;
        ///
        ///void main()
        ///{
        ///	if(distance(modelPos.xy, vec2(0.5)) &gt; 0.5)
        ///		discard;
        ///	gl_FragColor = vec4(color.x, color.y, color.z, pow(1.0 - distance(modelPos.xy, vec2(0.5)) * 2.0, 3.0));
        ///}.
        /// </summary>
        internal static string GlowingParticle {
            get {
                return ResourceManager.GetString("GlowingParticle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to uniform vec4 color;
        ///
        ///varying vec3 modelPos;
        ///
        ///void main()
        ///{
        ///	gl_FragColor = vec4(color.x, color.y, color.z, 1 - pow(min(min(modelPos.x, 1 - modelPos.x), min(modelPos.y, 1 - modelPos.y), 2));
        ///}.
        /// </summary>
        internal static string TileBorder {
            get {
                return ResourceManager.GetString("TileBorder", resourceCulture);
            }
        }
    }
}

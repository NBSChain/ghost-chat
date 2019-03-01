using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Meziantou.Framework.CodeDom;
using Newtonsoft.Json;

namespace Naruto.WPF2Generator
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var freeIconFile = Path.GetFullPath(Path.Combine("../../../", "Free", "metadata", "icons.json"));

            if (!File.Exists(freeIconFile))
            {
                Console.Error.WriteLine($"File not found: {freeIconFile}");
                Console.ReadKey();
                return;
            }

            var freeIcons = JsonConvert.DeserializeObject<IDictionary<string, Icon>>(File.ReadAllText(freeIconFile));
            var unit = new CompilationUnit();
            var ns = unit.AddNamespace("Naruto.WPF2Icon");

            {
                var enumeration = ns.AddType(new EnumerationDeclaration($"FontAwesomeIcons") { Modifiers = Modifiers.Public });
                enumeration.BaseType = typeof(ushort);
         
                foreach (var (key, value) in freeIcons)
                {
                    var identifier = ToCSharpIdentifier(PascalName(key));
                    var member = new EnumerationMember(identifier, value.UnicodeIntValue);
                    enumeration.Members.Add(member);

                    member.XmlComments.Add(new XElement("summary", $"{value.Label} ({value.Unicode})"));
                }
            }

            var codeGenerator = new CSharpCodeGenerator();
            File.WriteAllText("../../../Free/genout/FontAweIcons.cs", codeGenerator.Write(unit));
            return;
        }


        private static string PascalName(string name)
        {
            var sb = new StringBuilder();
            bool upperCase = true;
            foreach (char c in name)
            {
                if (c == '-')
                {
                    upperCase = true;
                    continue;
                }

                if (upperCase)
                {
                    sb.Append(char.ToUpper(c));
                }
                else
                {
                    sb.Append(c);
                }

                upperCase = false;
            }

            return sb.ToString();
        }

        private static string ToCSharpIdentifier(string name)
        {
            char c = name[0];
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_')
            {
                return name;
            }

            return "Icon" + name;
        }
    }

    internal class Icon
    {
        public string[] Styles { get; set; }
        public string Unicode { get; set; }
        public int UnicodeIntValue => int.Parse(Unicode, System.Globalization.NumberStyles.HexNumber);
        public string Label { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Creational.Builder
{
    //Notes
    //Motivation for creating builder pattern
    //1.) Some objects are simple and can be created in a single constructor call
    //2.) Other objects require a lot of ceremony
    //3.) Having an object with 10 constructors arguements is not procuctive
    //4.) Instead, opt for piecewise constrcution
    //5.) Builder provides an API for constructing an object step-by-step

    //When piecewise object construction is complicated, provide an API for doing it succinctly

    //A Builder is a separate component for  building an object 
    //can either give builder a constructor or return it via a static function
    //to make a builder fluent return 'this'
    //Different facets of an object can be built with different builders working in tandem via a base class
    internal class BuilderPattern
    {
        static void MainBP(string[] args)
        {
            //if you want to build a simple HTML paragraph using string Builder
            var hello = "hello";
            var strbuilder = new StringBuilder();
            strbuilder.Append("<p>");
            strbuilder.Append(hello);
            strbuilder.Append("</p>");
            Console.WriteLine(strbuilder);

            // now I want an HTML list with 2 words in it
            var words = new[] { "hello", "world" };
            strbuilder.Clear();
            strbuilder.Append("<ul>");
            foreach (var word in words)
            {
                strbuilder.AppendFormat("<li>{0}</li>", word);
            }
            strbuilder.Append("</ul>");
            Console.WriteLine(strbuilder);

            // ordinary non-fluent builder
            var builder = new HTMLBuilder("ul");
            builder.AddChild("li", "hello");
            builder.AddChild("li", "world");
            Console.WriteLine(builder.ToString());

            // fluent builder
            strbuilder.Clear();
            builder.Clear(); // disengage builder from the object it's building, then...
            builder.AddFluentChild("li", "hello").AddFluentChild("li", "world");
            Console.WriteLine(builder.ToString());

        }
    }

    class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int indentSize = 2;

        public HtmlElement()
        { }

        public HtmlElement(string name, string text)
        {
            Name = name;
            Text = text;
        }

        private string ToStringImpl(int indent)
        {
            StringBuilder sb = new StringBuilder();
            var indentation = new string(' ', indentSize * indent);
            sb.Append($"{indentation}<{Name}>\n");
            if(!string.IsNullOrWhiteSpace(Text)) {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.Append(Text);
                sb.Append("\n");
            }

            foreach(var element in Elements)
            {
                sb.Append(element.ToStringImpl(indent + 1));
            }

            sb.Append($"{indentation}<{Name}>\n");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }

    }

    class HTMLBuilder
    {
        private readonly string rootName;
        HtmlElement root = new HtmlElement();

        public HTMLBuilder(string rootName)
        {
            this.rootName = rootName;
            root.Name = this.rootName;
        }

        //Add Child
        public void AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
        }

        //Add Child Fluent
        public HTMLBuilder AddFluentChild(string childName, string childText) {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElement { Name = rootName };
        }
    }
}

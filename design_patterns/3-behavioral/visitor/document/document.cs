

using System.Text;

namespace VariableScope
{



    using System.Collections.Generic;

    public interface IDocumentElement
    {
        void Accept(IDocumentVisitor visitor);
    }

    public record Heading(string Text, int Level) : IDocumentElement
    {
        public void Accept(IDocumentVisitor visitor) => visitor.Visit(this);
    }
    public record Paragraph(string Text) : IDocumentElement
    {
        public void Accept(IDocumentVisitor visitor) => visitor.Visit(this);
    }
    public record Image(string Url, int Width) : IDocumentElement
    {
        public void Accept(IDocumentVisitor visitor) => visitor.Visit(this);
    }


    public class MyDoc
    {
        private List<IDocumentElement> _elements = new();
        public void Add(IDocumentElement element) => _elements.Add(element);
        public void Export(IDocumentVisitor visitor)
        {
            foreach (var element in _elements)
                element.Accept(visitor);
        }

    }

    public interface IDocumentVisitor
    {
        void Visit(Heading heading);
        void Visit(Paragraph paragraph);
        void Visit(Image image);


    }


    /*
    <h1>Introduction</h1>
    <p>This is a paragraph.</p>
    <img src="photo.jpg" width="800"/>
    */
    public class HtmlExportor : IDocumentVisitor
    {

        public void Visit(Heading heading) => Console.WriteLine($"<h{heading.Level}>{heading.Text}</h{heading.Level}>");

        public void Visit(Paragraph paragraph) => Console.WriteLine($"<p>{paragraph.Text}</p>");

        public void Visit(Image image) => Console.WriteLine($"<img src=\"{image.Url}\" width=\"{image.Width}\"/>");


    }


    /*
    # Introduction
    This is a paragraph.
    [Image: photo.jpg, 800px]
    */
    public class PlainTextExportor : IDocumentVisitor
    {
        public void Visit(Heading heading)
        {
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < heading.Level; i++)
            {
                sb.Append("#");
            }
            sb.Append($" {heading.Text}");
            Console.WriteLine(sb.ToString());

        }

        public void Visit(Paragraph paragraph) => Console.WriteLine(paragraph.Text);

        public void Visit(Image image) => Console.WriteLine($"[Image: {image.Url}, {image.Width}px]");

    }


    public class Program
    {
        public static void Main()
        {
            var docs = new List<IDocumentElement>() {
                new Heading("Introduction", 1), new Paragraph("This is a paragraph."), new Image("photo.jpg", 800), new Heading("Conclusion", 2), new Paragraph("This is the conclusion.")
            };

            var doc = new MyDoc();
            foreach (var element in docs)
                doc.Add(element);

            doc.Export(new HtmlExportor());
            doc.Export(new PlainTextExportor());


        }
    }
}
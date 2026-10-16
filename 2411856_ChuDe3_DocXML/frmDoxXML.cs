using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using _2411856_ChuDe3;

namespace _2411856_ChuDe3_DocXML
{
    public partial class frmDoxXML : Form
    {
        public frmDoxXML()
        {
            InitializeComponent();
        }

        private void btnDocXML_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load("books.xml");

            XmlNodeList nodeList =
                xmlDoc.DocumentElement.SelectNodes("/catalog/book");

            dgvBooks.Rows.Clear();

            foreach (XmlNode node in nodeList)
            {
                string isbn = node.Attributes["ISBN"].Value;

                string title =
                    node.SelectSingleNode("title").InnerText;

                string price =
                    node.SelectSingleNode("price").InnerText;

                string firstName =
                    node.SelectSingleNode("author/first-name").InnerText;

                string lastName =
                    node.SelectSingleNode("author/last-name").InnerText;

                string author = firstName + " " + lastName;

                dgvBooks.Rows.Add(
                    isbn,
                    title,
                    author,
                    price
                );

            }
            }

        private void btnGhiXML_Click(object sender, EventArgs e)
        {
            using (XmlWriter writer = XmlWriter.Create("books_new.xml"))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("book");

                writer.WriteAttributeString("ISBN", txtISBN.Text);

                writer.WriteAttributeString(
                    "yearpublished",
                    txtYear.Text
                );

                writer.WriteElementString(
                    "title",
                    txtTitle.Text
                );

                writer.WriteElementString(
                    "author",
                    txtAuthor.Text
                );

                writer.WriteElementString(
                    "price",
                    txtPrice.Text
                );

                writer.WriteEndElement();

                writer.WriteEndDocument();
            }

            MessageBox.Show("Ghi XML thành công!");
        }
        private void LuuDanhSach(List<Book> books)
        {
            XmlSerializer serializer =
                new XmlSerializer(typeof(List<Book>));

            using (var writer = new StreamWriter("books.xml"))
            {
                serializer.Serialize(writer, books);
            }
        }

        private void btnSerialize_Click(object sender, EventArgs e)
        {
            List<Book> books = new List<Book>();

            books.Add(new Book
            {
                ISBN = "9831123212",
                Title = "A Programmer's Guide to ADO .Net using C#",
                Author = "Mahesh Chand",
                Price = 44.99m,
                YearPublished = 2002
            });

            books.Add(new Book
            {
                ISBN = "9781484234",
                Title = "Pro Entity Framework Core 2",
                Author = "Adam Freeman",
                Price = 45.09m,
                YearPublished = 2019
            });

            XmlSerializer serializer =
                new XmlSerializer(typeof(List<Book>));

            using (var writer = new StreamWriter("books.xml"))
            {
                serializer.Serialize(writer, books);
            }

            MessageBox.Show("Đã ghi dữ liệu vào books.xml");

        }
        
    }
}

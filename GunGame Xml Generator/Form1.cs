using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GunGame_Xml_Generator
{
    public partial class GunGameGenerator : Form
    {
        static Random random;
        string zitemFilename;
        string stringFilename;
        string gungameFilename = "gungame.xml";
        public GunGameGenerator()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            random = new Random();
            zitemFilename = null;
            stringFilename = null;
            Status.Visible = false;           
        }

        public void Compare(string zitemFile, string gungameFile)
        {
            List<string> meleeIDs = new List<string>();
            List<string> primaryIDs = new List<string>();
            List<string> secondaryIDs = new List<string>();

            //CUSTOM MELEE
            using (XmlTextReader ZitemReader = new XmlTextReader(zitemFile))
            {
                while (ZitemReader.ReadToFollowing("ITEM"))
                {
                    ZitemReader.MoveToContent();
                    if (ZitemReader.NodeType == XmlNodeType.Element && ZitemReader.Name == "ITEM")
                    {
                        string slotName = ZitemReader.GetAttribute("slot");

                        if (!string.IsNullOrEmpty(slotName) && slotName.ToLower() == "melee")
                        {
                            
                            meleeIDs.Add(ZitemReader.GetAttribute("id"));
                           
                        }                                         

                    }
                }
            }

            //CUSTOM RANGE
            using (XmlTextReader ZitemReader = new XmlTextReader(zitemFile))
            {
                while (ZitemReader.ReadToFollowing("ITEM"))
                {
                    ZitemReader.MoveToContent();
                    if (ZitemReader.NodeType == XmlNodeType.Element && ZitemReader.Name == "ITEM")
                    {
                        string slotName = ZitemReader.GetAttribute("slot");

                        if (!string.IsNullOrEmpty(slotName) && slotName.ToLower() == "range")
                        {
                            primaryIDs.Add(ZitemReader.GetAttribute("id"));

                        }
                    }
                }
            }

            //CUSTOM RANGE
            using (XmlTextReader ZitemReader = new XmlTextReader(zitemFile))
            {
                while (ZitemReader.ReadToFollowing("ITEM"))
                {
                    ZitemReader.MoveToContent();
                    if (ZitemReader.NodeType == XmlNodeType.Element && ZitemReader.Name == "ITEM")
                    {
                        string slotName = ZitemReader.GetAttribute("slot");

                        if (!string.IsNullOrEmpty(slotName) && slotName.ToLower() == "range")
                        {

                            secondaryIDs.Add(ZitemReader.GetAttribute("id"));
                        }

                    }
                }
            }
            XDocument xmlDoc = new XDocument();
            for (int i = 1; i <= 3; i++)
            {
                List<XElement> xmlItems = GenerateRandomCombinations(meleeIDs, primaryIDs, secondaryIDs, 50);

                XDocument tempDoc = new XDocument(
                    new XDeclaration("1.0", "utf-8", null),
                    new XElement("XML",
                        new XAttribute("id", "gungame"),
                        new XElement("SET",
                            new XAttribute("id", i),
                            xmlItems
                        )
                    )
                );

                xmlDoc.Root?.Add(tempDoc.Root?.Elements());
                
                if (xmlDoc.Root == null)
                {
                    xmlDoc = tempDoc;
                }
            }
          
            xmlDoc.Save("gungame.xml");

        }

        List<XElement> GenerateRandomCombinations(List<string> melee, List<string> primaryIDs, List<string> secondaryIDs, int count)
        {
            List<XElement> result = new List<XElement>();
            int i = 1;
            while (i <= count)
            {
                string randomMelee = GetRandomElement(melee);
                string randomPrimaryID = GetRandomElement(primaryIDs);
                string randomSecondaryID = GetRandomElement(secondaryIDs);

                string meleeString = GetItemName(randomMelee);
                string primaryString = GetItemName(randomPrimaryID);
                string secondaryString = GetItemName(randomSecondaryID);    
                if (meleeString  == "nomsg" || meleeString.Contains("Training") || primaryString.Contains("Training") || secondaryString.Contains("Training"))
                {
                    continue;
                }
                XComment commentElement = new XComment($"{meleeString}, {primaryString}, {secondaryString}");
                XElement xmlItem = new XElement("ITEMSET",
                    commentElement,
                    new XAttribute("melee", randomMelee),
                    new XAttribute("primary", randomPrimaryID),
                    new XAttribute("secondary", randomSecondaryID)
                );
                result.Add(xmlItem);
                i++;
            }

            return result;
        }

        string GetItemName(string itemname)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(zitemFilename);

                // Create a namespace manager to handle the XML namespace
                XmlNamespaceManager nsManager = new XmlNamespaceManager(xmlDoc.NameTable);
                nsManager.AddNamespace("ns", "http://tempuri.org/zitem.xsd");

                // Construct XPath expression with namespace for finding the element with the specified item ID
                string xpathExpression = $"//ns:ITEM[@id='{itemname}']/@name";

                XmlNodeList nodes = xmlDoc.SelectNodes(xpathExpression, nsManager);

                if (nodes.Count > 0)
                {
                    if(nodes[0].Value.Contains("STR:"))
                    {
                        XmlDocument xmlDoc2 = new XmlDocument();
                        xmlDoc2.Load(stringFilename);

                        string strId = nodes[0].Value.Replace("STR:", "");

                        // Assuming xmlDoc is the XmlDocument object
                        XmlNode strNode = xmlDoc2.SelectSingleNode($"//STR[@id='{strId}']");

                        if (strNode != null)
                        {
                            return strNode.InnerText;
                        }
                        else
                        {
                            return "nomsg";
                        }
                    }
                    else
                    {
                        return nodes[0].Value;
                    }
                    
                }
                else
                {
                    // Item with the specified ID not found
                    return "Item not found";
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during XML parsing
                Console.WriteLine($"Error: {ex.Message}");
                return "Error occurred";
            }
        }

        static string GetRandomElement(List<string> list)
        {
            if (list.Count > 0)
            {
                int randomIndex = random.Next(list.Count);
                return list[randomIndex];
            }
            else
            {
                return string.Empty;
            }
        }

        private void ChooseZItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Select XML File";
            openFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {                
                zitemFilename = openFileDialog.FileName;
            }

        }

        private void ChooseString_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Select XML File";
            openFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                stringFilename = openFileDialog.FileName;
                
            }

        }

      

        private void StartProcess_Click(object sender, EventArgs e)
        {
            if (File.Exists(gungameFilename))
                File.Delete(gungameFilename);
            if (zitemFilename != null && stringFilename != null) {
                Compare(zitemFilename, gungameFilename);
                Status.Visible = true;

            }
            else
            {
                if(string.IsNullOrEmpty(zitemFilename))
                {
                    MessageBox.Show("Please select zitem.xml");
                }
                else
                {
                    MessageBox.Show("Please select string.xml");
                }
            }
            
        }
    }
}

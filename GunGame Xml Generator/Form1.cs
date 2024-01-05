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
    public partial class Form1 : Form
    {
        static Random random;
        public Form1()
        {
            InitializeComponent();
             random = new Random();
            string zitemFilename = "zitem.xml";
            string gungameFilename = "gungame.xml";
            if (File.Exists(gungameFilename))
                File.Delete(gungameFilename);
            Compare(zitemFilename, gungameFilename);
            MessageBox.Show($"Generated {gungameFilename} successfully.");
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

        static List<XElement> GenerateRandomCombinations(List<string> melee, List<string> primaryIDs, List<string> secondaryIDs, int count)
        {
            List<XElement> result = new List<XElement>();

            for (int i = 1; i <= count; i++)
            {
                string randomMelee = GetRandomElement(melee);
                string randomPrimaryID = GetRandomElement(primaryIDs);
                string randomSecondaryID = GetRandomElement(secondaryIDs);

                XElement xmlItem = 
                    new XElement("ITEMSET",
                        new XAttribute("melee", randomMelee),
                        new XAttribute("primary", randomPrimaryID),
                        new XAttribute("secondary", randomSecondaryID)
                    
                );

                result.Add(xmlItem);
            }

            return result;
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
    }
}

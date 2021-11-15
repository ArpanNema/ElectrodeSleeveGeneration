using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Rulyotano.Math;

namespace SvgManipulationLogic
{
    public class SVGData
    {
        public string OutputPath { get; set; }
        public SVGData()
        {
            OutputPath = @"C:\Users\arpan_svgrhg2\OneDrive\Desktop\Project\POC\CSharp\GenerateFiles\output.svg";
        }
        public bool generateSVGFile(string path,string gridId,string id,string d) 
        {
            bool status = false;
            try
            {
                XDocument document = XDocument.Load(path);
                XElement svg_Element = document.Root;
                IEnumerable<XElement> componentList = (from e1 in svg_Element.Elements("{http://www.w3.org/2000/svg}g")
                                                      select e1).ToList();
                XElement ele=componentList.Where(x=>x.Attribute("id").Value==gridId).FirstOrDefault()
                    .Descendants().Where(x => x.Attribute("id").Value == id).First();
                ele.SetAttributeValue("d", d);
                document.Save(OutputPath);
                //document.Save(@"C:\Users\arpan_svgrhg2\OneDrive\Desktop\Project\POC\CSharp\GenerateFiles\output.svg");

            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        public bool generateSVGFileWithData(string path,string id, string data)
        {
            bool status = false;
            try
            {
                XDocument document = XDocument.Load(path);
                XElement svg_Element = document.Root;
                IEnumerable<XElement> componentList = (from e1 in svg_Element.Elements("{http://www.w3.org/2000/svg}g")
                                                       select e1).ToList();
                XElement ele = componentList.Where(x => x.Attribute("id").Value == id).FirstOrDefault();
                var txt = XElement.Parse(data);
                ele.Add(txt);
                document.Save(OutputPath);
                //document.Save(@"C:\Users\arpan_svgrhg2\OneDrive\Desktop\Project\POC\CSharp\GenerateFiles\output.svg");

            }
            catch (Exception e)
            {
                status = false;
            }
            return status;
        }
        public XElement GetElementById(XDocument document, string id) 
        {
            XElement svg_Element = document.Root;
            IEnumerable<XElement> componentList = (from e1 in svg_Element.Elements("{http://www.w3.org/2000/svg}g")
                                          select e1).ToList();
            return componentList.Where(x => x.Attribute("id").Value == id).First();
        }
        public bool UpdateElementStyle(XDocument document, string id,string style) 
        {
            XElement svg_Element = document.Root;
            IEnumerable<XElement> componentList = (from e1 in svg_Element.Elements("{http://www.w3.org/2000/svg}g")
                                                   select e1).ToList();
            var pathElement= componentList.First().Descendants().Where(x => x.Attribute("id").Value == id).First();
            pathElement.SetAttributeValue("style", style);
            return true;

        }
        public void Test() { 

        }
    }
}

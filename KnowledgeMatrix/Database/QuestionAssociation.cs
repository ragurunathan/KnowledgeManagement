using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace KnowledgeMatrix.Database
{
     public class QuestionAssocation
    {
        [XmlElement(ElementName = "SNo")]
        public string SNo;
        [XmlElement(ElementName = "ParentQuesNo")]
        public int ParentQuesNo;
        [XmlElement(ElementName = "ChildQuesNo")]
        public int ChildQuesNo;
        [XmlElement(ElementName = "TotalQuestions")]
        public int TotalQuestions;
    }
     [XmlTypeAttribute(AnonymousType = true)]
     public class QuestionAssocationData
     {
         [XmlElement("QuestionAssociation")]
         public List<QuestionAssocation> objQuestionMas { get; set; }

         public QuestionAssocationData()
         {
             objQuestionMas = new List<QuestionAssocation>();
         }
     }

}

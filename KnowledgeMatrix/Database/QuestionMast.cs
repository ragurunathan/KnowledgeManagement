using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace KnowledgeMatrix.Database
{
    public class QuestionMast
    {
        [XmlElement(ElementName = "QuesNo")]
        public int QuesNo;

        [XmlElement(ElementName = "Name")]
        public string Name;


        [XmlElement(ElementName = "ParentQuestionNo")]
        public int ParentQuestionNo;

        [XmlElement(ElementName = "ParentParentQuestionNo")]
        public int ParentParentQuestionNo;

        [XmlElement(ElementName = "TotalQuestions")]
        public int TotalQuestions;

        [XmlElement(ElementName = "isLeaf")]
        public Boolean isLeaf;

        [XmlElement(ElementName = "QuesBank")]
        public string QuesBank;

        [XmlElement(ElementName = "eTutor")]
        public string eTutor;

        [XmlElement(ElementName = "QuesBankGen")]
        public string QuesBankGen;

        [XmlElement(ElementName = "MockTest")]
        public string MockTest;

        [XmlElement(ElementName = "QuesBankDate")]
        public string QuesBankDate;

    }

    [XmlTypeAttribute(AnonymousType = true)]
    public class QuestionsData
    {
        [XmlElement("QuestionMaster")]
        public List<QuestionMast> objQuestionMas { get; set; }

        public QuestionsData()
        {
            objQuestionMas = new List<QuestionMast>();
        }
    }

   
}

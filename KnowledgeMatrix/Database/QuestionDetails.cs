using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
// Does XML serializing for a class.
using System.Drawing;            // Required for storing a Bitmap.
// Required for using Memory stream objects.
using System.ComponentModel;
using System.Text;
using System;     // Required for conversion of Bitmap objects.


namespace KnowledgeMatrix.Database
{
     [Serializable]
    public class PictureColl
    {

    }
     [Serializable]
    public class QuestionDetail
    {
        private Bitmap picture;

        private Bitmap picture1;

        private Bitmap picture2;

        private Bitmap picture3;

        [XmlElement(ElementName = "QuesNo")]
        public int QuesNo;

        [XmlElement(ElementName = "Question")]
        public string Question;

        [XmlElement(ElementName = "AnswerType")]
        public string AnswerType;

        [XmlElement(ElementName = "Answer")]
        public string Answer;

        [XmlElement(ElementName = "QuestionOptions")]
        public string QuestionOptions;

        [XmlElement(ElementName = "Complexity")]
        public string Complexity;

        [XmlElement(ElementName = "Pattern")]
        public string Pattern;

        [XmlElement(ElementName = "CorrectAnswerDetails")]
        public string CorrectAnswerDetails;

       

        [XmlElement(ElementName = "AnswerConcept")]
        public string AnswerConcept;

        [XmlElement(ElementName = "QuesDetSNo")]
        public int QuesDetSNo;

        // Set serialization to IGNORE this property because the 'PictureByteArray' property
        // is used instead to serialize the 'Picture' Bitmap as an array of bytes.
        [XmlIgnoreAttribute()]
        public Bitmap Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        // Serializes the 'Picture' Bitmap to XML.
        [XmlElementAttribute("Picture")]
        public byte[] PictureByteArray
        {
            get
            {
                if (picture != null)
                {
                    TypeConverter BitmapConverter = TypeDescriptor.GetConverter(picture.GetType());
                    return (byte[])BitmapConverter.ConvertTo(picture, typeof(byte[]));
                }
                else
                    return null;
            }

            set
            {
                if (value != null)
                    picture = new Bitmap(new MemoryStream(value));
                else
                    picture = null;
            }
        }
        // Set serialization to IGNORE this property because the 'PictureByteArray' property
        // is used instead to serialize the 'Picture' Bitmap as an array of bytes.
        [XmlIgnoreAttribute()]
        public Bitmap Picture1
        {
            get { return picture1; }
            set { picture1 = value; }
        }

        // Serializes the 'Picture' Bitmap to XML.
        [XmlElementAttribute("Picture1")]
        public byte[] PictureByteArray1
        {
            get
            {
                if (picture1 != null)
                {
                    TypeConverter BitmapConverter = TypeDescriptor.GetConverter(picture1.GetType());
                    return (byte[])BitmapConverter.ConvertTo(picture1, typeof(byte[]));
                }
                else
                    return null;
            }

            set
            {
                if (value != null)
                    picture1 = new Bitmap(new MemoryStream(value));
                else
                    picture1 = null;
            }
        }

        // Set serialization to IGNORE this property because the 'PictureByteArray' property
        // is used instead to serialize the 'Picture' Bitmap as an array of bytes.
        [XmlIgnoreAttribute()]
        public Bitmap Picture2
        {
            get { return picture2; }
            set { picture2 = value; }
        }

        // Serializes the 'Picture' Bitmap to XML.
        [XmlElementAttribute("Picture2")]
        public byte[] PictureByteArray2
        {
            get
            {
                if (picture2 != null)
                {
                    TypeConverter BitmapConverter = TypeDescriptor.GetConverter(picture2.GetType());
                    return (byte[])BitmapConverter.ConvertTo(picture2, typeof(byte[]));
                }
                else
                    return null;
            }

            set
            {
                if (value != null)
                    picture2 = new Bitmap(new MemoryStream(value));
                else
                    picture2 = null;
            }
        }


        // Set serialization to IGNORE this property because the 'PictureByteArray' property
        // is used instead to serialize the 'Picture' Bitmap as an array of bytes.
        [XmlIgnoreAttribute()]
        public Bitmap Picture3
        {
            get { return picture3; }
            set { picture3 = value; }
        }

        // Serializes the 'Picture' Bitmap to XML.
        [XmlElementAttribute("Picture3")]
        public byte[] PictureByteArray3
        {
            get
            {
                if (picture3 != null)
                {
                    TypeConverter BitmapConverter = TypeDescriptor.GetConverter(picture3.GetType());
                    return (byte[])BitmapConverter.ConvertTo(picture3, typeof(byte[]));
                }
                else
                    return null;
            }

            set
            {
                if (value != null)
                    picture3 = new Bitmap(new MemoryStream(value));
                else
                    picture3 = null;
            }
        }

        
        [XmlElement(ElementName = "QuestionOptions1")]
        public string OptionOne;

        [XmlElement(ElementName = "QuestionOptions2")]
        public string OptionTwo;

        [XmlElement(ElementName = "QuestionOptions3")]
        public string OptionThree;

        [XmlElement(ElementName = "QuestionOptions4")]
        public string OptionFour;

        [XmlElement(ElementName = "AnswerResponse")]
        public string AnswerResponse;

        [XmlElement(ElementName = "AnsRespType")]
        public string AnsRespType;

        [XmlElement(ElementName = "CategoryName")]
        public string CategoryName;

        [XmlElement(ElementName = "ModuleName")]
        public string ModuleName;

    }
    [XmlTypeAttribute(AnonymousType = true)]
    [Serializable]
    public class QuestionDetailData
    {
        [XmlElement("QuestionDetail")]
        public List<QuestionDetail> objQuestionDetail { get; set; }

        [XmlElement("QuestionType")]
        public string QuestionType { get; set; }

        public QuestionDetailData()
        {
            objQuestionDetail = new List<QuestionDetail>();
        }
    }
}

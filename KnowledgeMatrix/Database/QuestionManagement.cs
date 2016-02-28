using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace KnowledgeMatrix.Database
{

    public class ResultList
    {
        
        public string UserName;
        public string FileName;
        public string TestSatus;
        public string TestMark;
        public string DateTaken;
        public string ExamName;
            }
    public class QuestionManagement
    {
        public int QuestionMngNo { get; set; }
        public string ExamName { get; set; }
        public string QuestionTopic{ get; set; }
        public string Subject{ get; set; }
        public string ExamPasPercentageScore { get; set; }
        public string ExamMode { get; set; }
        public string QuestionComplexity { get; set; }
        public string QuestionType { get; set; }
        public string TotalQuestions { get; set; }
        public string FileName { get; set; }
        public string TestStatus { get; set; }
        public string TestResult { get; set; }
        public string TestTime { get; set; }
        public string MockTestDate { get; set; }
        public List<ResultList> objResultList { get; set; }
    }
    public class QuestionManagementColl
    {
        public List<QuestionManagement> objQuestionManagement { get; set; }
        public QuestionManagementColl()
        {
            objQuestionManagement = new List<QuestionManagement>();
        }
    }

    public class QuestionManagementWithQues : QuestionManagement
    {
        [XmlElement("QuestionDetail")]
        public List<QuestionDetail> objQuestionDetail { get; set; }
    }
    public class QuestionManagementWithQuesColl
    {
        public List<QuestionManagementWithQues> objQuestionManagement { get; set; }
        public QuestionManagementWithQuesColl()
        {
            objQuestionManagement = new List<QuestionManagementWithQues>();
        }
    }


}

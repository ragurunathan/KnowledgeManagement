using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeMatrix.Database
{
    public class QuestionPap
    {
        public int QuestionNo { get; set; }
        public bool Select { get; set; }
        public string Name { get; set; }

        public int Questions{ get; set; }
        
        public string Weightage{ get; set; }
        public int QuestionTypeAva{ get; set; }
        public int QCompQAval{ get; set; }
        public string AvaWeitage{ get; set; }
        
        public string AlterWeitage{ get; set; }
        public int AlterQNo{ get; set; }

        public string Flag { get; set; }

    }
    public class QuestionPaperColl
    {
        public List<QuestionPap> objPaper { get; set; }
        public QuestionPaperColl()
        {
            objPaper = new List<QuestionPap>();
        }

    }

    public class TestResults
    {
        public string TestResult { get; set; }
        public Int32 QuestionAvlbl { get; set; }
        public Int32 QuestionAnswered { get; set; }
        public double CorrectAnswerPer { get; set; }

    }
    public class TestResultColl
    {
        public List<TestResults> objTestResults { get; set; }
        public TestResultColl()
        {
            objTestResults = new List<TestResults>();
        }
    }
}

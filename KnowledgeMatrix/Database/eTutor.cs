using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeMatrix.Database
{
    [Serializable]
    public class PageDet
    {
        public int PageNmbr { get; set; }
        public string PageInfo { get; set; }
    }
    

    [Serializable]
    public class eTutorMast
    {

        public List<PageDet> Pages { get; set; }
        public string CategoryName { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public int QuesNo { get; set; }
        public int NoOfQuestions { get; set; }
        public QuestionDetailData QuestionDetailData { get; set; }

        public eTutorMast()
        {
            Pages = new List<PageDet>();
            QuestionDetailData = new QuestionDetailData();
        }

    }

    [Serializable]
    public class eTutorCollData
    {
        public List<eTutorMast> eTutorlst { get; set; }
        public eTutorCollData()
        {
            eTutorlst = new List<eTutorMast>();
        }

    }
}

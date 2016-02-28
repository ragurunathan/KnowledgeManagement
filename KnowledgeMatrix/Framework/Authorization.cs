using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeMatrix.Framework
{

    public class OperationNames
    {
        public enum TypeOfOperations
        {
            eTutor_Manage,
            Question_Manage,
            CBT_Exam,
            Mock_Exam,
            Mock_Exam_Multiple
        };
        //public const string eTutor_Manage = "eTutor-Add / Edit / Delete of new images";
        //public const string Question_Manage = "Question-Add / Edit / Delete of nodes/questions";
        //public const string CBT_Exam = "Question-CBT";
        //public const string Mock_Exam = "Question-Mock";
        //public const string Mock_Exam_Multiple = "Mock-Multiple";
    }

     public class AuthorizationOperations : OperationNames
    {
        public List<string> objListOfOperations = new List<string>();

       
        public List<string> StandardOperations()
        {
            objListOfOperations.Clear();
            return objListOfOperations;
        }

        public List<string> ProfessionalOperations()
        {
            objListOfOperations.Clear();
            objListOfOperations.Add(TypeOfOperations.eTutor_Manage.ToString());
            objListOfOperations.Add(TypeOfOperations.Question_Manage.ToString());
            objListOfOperations.Add(TypeOfOperations.CBT_Exam.ToString());
            objListOfOperations.Add(TypeOfOperations.Mock_Exam.ToString());
            objListOfOperations.Add(TypeOfOperations.Mock_Exam_Multiple.ToString());
            return objListOfOperations;
        }
        public List<string> AdminOperations()
        {
            objListOfOperations.Clear();
            objListOfOperations.Add(TypeOfOperations.eTutor_Manage.ToString());
            objListOfOperations.Add(TypeOfOperations.Question_Manage.ToString());
            objListOfOperations.Add(TypeOfOperations.CBT_Exam.ToString());
            objListOfOperations.Add(TypeOfOperations.Mock_Exam.ToString());
            objListOfOperations.Add(TypeOfOperations.Mock_Exam_Multiple.ToString());
            return objListOfOperations;
        }
        public bool isUserAccessible(TypeOfOperations objOperation)
        {
            bool UserAccess = true;

            bool isAdmin = false;
            isAdmin = Convert.ToBoolean(KnowledgeMatrix.Properties.Settings.Default.Administrator);

            List<string> obj;

            if (isAdmin)
            {
                obj = ProfessionalOperations();
                //if (Utility.isStandard())
                //{
                                                   
                //}
                //else
                //{
                    
                //}
               
            }
            else
            {
                obj = StandardOperations();  
                //Client
               // UserAccess = false;
            }

            if (obj.Contains(objOperation.ToString()))
                UserAccess = true;
            else
                UserAccess = false;

            return UserAccess;
        }
    }
 
}

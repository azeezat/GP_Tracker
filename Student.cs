using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP_Tracker
{
    

    public class Student
    {
        public int units, points;
        public double weights, gp;
        public string course;
        public Grade grade;

        public int Unit
        {
            get { return units; }
            set { value = units; }
        }

        public string Course
        {
            get { return course; }
            set { value = course; }
        }


        public enum Grade { F, E, D, C, B, A};
        
    }
}


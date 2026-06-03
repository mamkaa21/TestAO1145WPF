using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAO1145WPF.Model;

namespace TestAO1145WPF.ViewModel
{
    public class AddTeacherWinVM: BaseVM
    {
        private Teacher teacher { get; set; }
        public Teacher Teacher
        {
            get => teacher;
            set
            {
                teacher = value;
                Signal(nameof(Teacher));
            }
        }
        private List<Teacher> teacherList { get; set; }
        public List<Teacher> TeacherList
        {
            get => teacherList;
            set
            {
                teacherList = value;
                Signal(nameof(TeacherList));
            }
        }
        private Student student { get; set; }
        public Student Student
        {
            get => student;
            set
            {
                student = value;
                Signal(nameof(Student));
            }
        }
        private List<Student> studentList { get; set; }
        public List<Student> StudentList
        {
            get => studentList;
            set
            {
                studentList = value;
                Signal(nameof(StudentList));
            }
        }
        private Subject subject { get; set; }
        public Subject Subject
        {
            get => subject;
            set
            {
                subject = value;
                Signal(nameof(Subject));
            }
        }
        private List<Subject> subjectList { get; set; }
        public List<Subject> SubjectList
        {
            get => subjectList;
            set
            {
                subjectList = value;
                Signal(nameof(SubjectList));
            }
        }
        public AddTeacherWinVM()
        {

        }
    }
}

using IntelligentClass.ViewModel.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WpfCollectionDemo1.mvvm的使用.ViewModel
{
    class ListBox滚动条样式VM
    {

        private ObservableCollection<SubjectName> subjectNames = new ObservableCollection<SubjectName>();
        public ObservableCollection<SubjectName> SubjectNames
        {
            set
            {
                if (subjectNames != null)
                {
                    subjectNames = value;
                }
            }
            get
            {
                return subjectNames;
            }
        }


        public ListBox滚动条样式VM()
        {
            SubjectNames= NewMethod();
        }

        public ObservableCollection<SubjectName> NewMethod()
        {

            ObservableCollection<SubjectName> subjectNames = new ObservableCollection<SubjectName>();
            for (int i = 0; i < 50; i++)
            {
                subjectNames.Add(new SubjectName()
                {
                    SubjectClass = "语",
                    SubjectClassName = "语文",
                    subjectChinaLua = "yuwen",
                    SubjectClassImage = "IMAGE_canvas_subject_1",
                    SubjectClassNameColor = "#b5babf"
                });
            }
           
            subjectNames.Add(new SubjectName()
            {
                SubjectClass = "数",
                SubjectClassName = "数学",
                subjectChinaLua = "shuxue",
                SubjectClassImage = "IMAGE_canvas_subject_1",
                SubjectClassNameColor = "#b5babf"
            });
            subjectNames.Add(new SubjectName()
            {
                SubjectClass = "英",
                SubjectClassName = "英语",
                subjectChinaLua = "yingyu",
                SubjectClassImage = "IMAGE_canvas_subject_1",
                SubjectClassNameColor = "#b5babf"
            });
            subjectNames.Add(new SubjectName()
            {
                SubjectClass = "美",
                SubjectClassName = "美术",
                subjectChinaLua = "meishu",
                SubjectClassImage = "IMAGE_canvas_subject_1",
                SubjectClassNameColor = "#b5babf"
            });
            subjectNames.Add(new SubjectName()
            {
                SubjectClass = "音",
                SubjectClassName = "音乐",
                subjectChinaLua = "yinyue",
                SubjectClassImage = "IMAGE_canvas_subject_1",
                SubjectClassNameColor = "#b5babf"
            });
            subjectNames.Add(new SubjectName()
            {
                SubjectClass = "体",
                SubjectClassName = "体育",
                subjectChinaLua = "tiyu",
                SubjectClassImage = "IMAGE_canvas_subject_1",
                SubjectClassNameColor = "#b5babf"
            });
            subjectNames.Add(new SubjectName()
            {
                SubjectClass = "思",
                SubjectClassName = "思想品德",
                subjectChinaLua = "pinde",
                SubjectClassImage = "IMAGE_canvas_subject_1",
                SubjectClassNameColor = "#b5babf"
            });
            subjectNames.Add(new SubjectName()
            {
                SubjectClass = "生",
                SubjectClassName = "生物"
                ,
                subjectChinaLua = "shengwu",
                SubjectClassImage = "IMAGE_canvas_subject_1",
                SubjectClassNameColor = "#b5babf"
            });

            return subjectNames;
        }
    }
}

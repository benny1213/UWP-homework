using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace 英语学习系统.Model
{
    public class Student:INotifyPropertyChanged
    {

        private string _Sno;

        public string Sno
        {
            get { return this._Sno; }
            set
            {
                this._Sno = value;
                OnPropertyChanged("Sno");
            }
        }
        private string _Sname;
        public string Sname
        {
            get { return this._Sname; }
            set
            {
                this._Sname = value;
                OnPropertyChanged("Sname");
            }
        }

        private string _Pass;
        public string Pass
        {
            get { return this._Pass; }
            set
            {
                this._Pass = value;
                OnPropertyChanged("Pass");
            }
        }
        private string _Depart;
        public string Depart
        {
            get { return this._Depart; }
            set
            {
                this._Depart = value;
                OnPropertyChanged("Depart");
            }
        }

        private string _Simg;
        public string Simg
        {
            get { return this._Simg; }
            set
            {
                this._Simg = value;
                OnPropertyChanged("Simg");
            }
        }

        private BitmapImage _ImgSource;
        public BitmapImage ImgSource
        {
            get { return this._ImgSource; }
            set
            {
                this._ImgSource = value;
                OnPropertyChanged("ImgSource");
            }
        }
        private async void SetImg(string imgfile)
        {
            try
            {
                //操作文件夹
                StorageFolder folder = Windows.Storage.KnownFolders.PicturesLibrary;
                StorageFile file = await (await folder.GetFolderAsync("studydata")).GetFileAsync(imgfile);
                var filestream = await file.OpenAsync(FileAccessMode.Read);
                //BitmapImage img = new BitmapImage();
                //img.SetSource(filestream);

                //ImgSource = img;


                ImgSource = new BitmapImage();
                ImgSource.SetSource(filestream);


                //StorageFile file1 = await (await folder.GetFolderAsync("studydata")).GetFileAsync("big_buck_bunny.mp4");
                //var filestream1 = await file1.OpenAsync(FileAccessMode.Read);

                //MediaSource ms = MediaSource.CreateFromStream(filestream1, "");
                //this.MyAudioSource = ms;

            }
            catch
            {
                ImgSource = null;
            }
        }


        public Student(string sno, string sname, string pass, string depart, string img)
        {
            this.Sno = sno;
            this.Sname = sname;
            this.Pass = pass;
            this.Depart = depart;
            this.Simg = img;
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

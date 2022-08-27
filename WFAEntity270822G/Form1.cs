using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFAEntity270822G
{
    public partial class Form1 : Form
    {
        NorthwindEntities db = new NorthwindEntities();
        public Form1()
        {
            InitializeComponent();
        }


        private void btnSorgu1_Click(object sender, EventArgs e)
        {
            //Çalışanların isim soyisim ünvan doğumtarihi bilgilerini getiriniz.

            #region LinqToEntity

            //dgwList.DataSource = db.Employees.Select(a => new
            //{
            //    İsim = a.FirstName,
            //    Soyisim = a.LastName,
            //    Ünvan = a.Title,
            //    DoğumTarihi = a.BirthDate
            //}).ToList();

            #endregion

            #region LinqToSQL

            //SELECT FirstName, LastName, Title, BirthDate FROM Employees

            var result = from emp in db.Employees
                         select new
                         {
                             İsim = emp.FirstName,
                             Soyisim = emp.LastName,
                             Ünvan = emp.Title,
                             DoğumTarihi = emp.BirthDate
                         };

            dgwList.DataSource = result.ToList();

            #endregion
        }

        private void btnSorgu2_Click(object sender, EventArgs e)
        {
            //Çalışanların ID'si 2 ve 8 arasında olanların A-Z sıralanacak şekilde ID,AD,SOYAD,UNVAN bilgilerini listeleyiniz.

            #region LinqToEntity

            //dgwList.DataSource = db.Employees.Select(a => new
            //{
            //    ID = a.EmployeeID,
            //    İsim = a.FirstName,
            //    Soyisim = a.LastName,
            //    Ünvan = a.Title
            //}).Where(a => a.ID >= 2 && a.ID < 8).OrderBy(z => z.İsim).ToList();


            #endregion

            #region LinqToSQL

            var result = from emp in db.Employees
                         where emp.EmployeeID >= 2 && emp.EmployeeID < 8
                         orderby emp.FirstName
                         select new
                         {
                             ID = emp.EmployeeID,
                             İsim = emp.FirstName,
                             Soyisim = emp.LastName,
                             Ünvan = emp.Title
                         };

            dgwList.DataSource = result.ToList();

            #endregion
        }

        private void btnSorgu3_Click(object sender, EventArgs e)
        {
            //1960 yılında doğan çalışanların adı, soyadı, doğum tarihini getiriniz.

            #region LinqToEntity

            //1.YOL

            //dgwList.DataSource = db.Employees.Select(a => new
            //{
            //    İsim = a.FirstName,
            //    Soyisim = a.LastName,
            //    DogumTarihi = a.BirthDate
            //}).Where(a => a.DogumTarihi.Value.Year == 1960).ToList();

            //2.YOL

            //dgwList.DataSource = db.Employees.Select(a => new
            //{
            //    İsim = a.FirstName,
            //    Soyisim = a.LastName,
            //    DogumTarihi = a.BirthDate
            //}).Where(a => a.DogumTarihi.Value.Equals(1960)).ToList();

            //3.YOL

            //dgwList.DataSource = db.Employees.Select(a => new
            //{
            //    İsim = a.FirstName,
            //    Soyisim = a.LastName,
            //    DogumTarihi = a.BirthDate
            //}).Where(a => SqlFunctions.DatePart("YEAR", a.DogumTarihi) == 1960).ToList();


            #endregion

            #region LinqToSQL

            var result = from emp in db.Employees
                         where emp.BirthDate.Value.Year == 1960
                         select new
                         {

                             İsim = emp.FirstName,
                             Soyisim = emp.LastName,
                             DogumTarihi = emp.BirthDate
                         };

            dgwList.DataSource = result.ToList();

            #endregion
        }

        private void btnSorgu4_Click(object sender, EventArgs e)
        {
            //60 yaşından büyük olan çalışanların Adı, Soyadını , Doğum tarihini, A'dan Z'ye sıralayınız bilgilerini getiren linq to entity ve linq to sql sorgularını yazınız

            #region LinqToEntity

            //dgwList.DataSource = db.Employees.Select(a => new
            //{
            //    a.FirstName,
            //    a.LastName,
            //    a.BirthDate,
            //    Yas = SqlFunctions.DateDiff("YEAR", a.BirthDate, DateTime.Now)
            //}).Where(a => SqlFunctions.DateDiff("YEAR", a.BirthDate, DateTime.Now) > 60).OrderBy(a => a.FirstName).ToList();


            #endregion

            #region LinqToSQL

            var result = from emp in db.Employees
                         where SqlFunctions.DateDiff("YEAR", emp.BirthDate, DateTime.Now) > 60
                         select new
                         {
                             İsim = emp.FirstName,
                             Soyisim = emp.LastName,
                             DogumTarihi = emp.BirthDate,
                             Yas = SqlFunctions.DateDiff("YEAR", emp.BirthDate, DateTime.Now)
                         };

            dgwList.DataSource = result.ToList();

            #endregion

        }

        private void btnSorgu5_Click(object sender, EventArgs e)
        {
            //Doğum tarihi 1930 ile 1960 arasında olan ve USA'da oturan çalışanların bilgilerini getiren linq to entity ve linq to sql sorgularını yazınız.

            #region LinqToEntity

            dgwList.DataSource = db.Employees.Where(a => a.BirthDate.Value.Year > 1930 && a.BirthDate.Value.Year < 1960 && a.Country == "USA").ToList();


            #endregion

            #region LinqToSQL

            var result = from emp in db.Employees
                         where (emp.BirthDate.Value.Year > 1930 && emp.BirthDate.Value.Year < 1960 && emp.Country == "USA")
                         select emp;

            dgwList.DataSource = result.ToList();

            #endregion
        }

        private void btnSorgu6_Click(object sender, EventArgs e)
        {
            //Kategorilerime göre stok durum nasıl? bilgisini getiren linq to entity ve linq to sql sorgularını yazınız.

            #region LinqToEntity

            //dgwList.DataSource = db.Products.GroupBy(a => a.Category.CategoryName).Select(b => new
            //{
            //    b.Key,
            //    ToplamStok = b.Sum(c => c.UnitsInStock)
            //}).ToList();


            #endregion

            #region LinqToSQL

            var result = from prod in db.Products
                         group prod by prod.Category.CategoryName into g
                         select new
                         {
                             KategoriAdi = g.Key,
                             ToplamStok = g.Sum(c => c.UnitsInStock)
                         };

            dgwList.DataSource = result.ToList();

            #endregion
        }
    }
}

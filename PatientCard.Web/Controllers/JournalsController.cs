using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PatientCard.Model.Models;
using PatientCard.Web.ViewModel;

namespace PatientCard.Web.Controllers
{
    public class JournalsController : Controller
    {
        private PatientCardContext db = new PatientCardContext();

        // GET: Journals
        public ActionResult Index(DateTime? date1, DateTime? date2)
        {
            ViewBag.Date1 = date1;
            ViewBag.Date2 = date2;
            if ((ViewBag.Date1 != null) && (ViewBag.Date2 != null))
            {
                return View(db.Journals.Where(a => (date1 <= a.DateVisit) && (a.DateVisit <= date2)).ToList());
            }
            else
            {
                return View(db.Journals.ToList());
            }
        }

        // GET: Journals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journal journal = db.Journals.Find(id);
            if (journal == null)
            {
                return HttpNotFound();
            }
            return View(journal);
        }

        // GET: Journals/Create
        public ActionResult Create()
        {
            ViewBag.Patients = db.Patients.ToList();
            ViewBag.Doctors = db.Doctors.ToList();
            return View();
        }

        // POST: Journals/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientId,DoctorId,Diagnosis,DateVisit")] JournalViewModel jvm)
        {
            if (ModelState.IsValid)
            {
                Patient patient = db.Patients.Find(jvm.PatientId);
                Doctor doctor = db.Doctors.Find(jvm.DoctorId);
                Journal journal = new Journal {Patient = patient, Doctor=doctor,  Diagnosis = jvm.Diagnosis, DateVisit = jvm.DateVisit};
                db.Journals.Add(journal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jvm);
        }

        // GET: Journals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Journal journal = db.Journals.Find(id);

            if (journal == null)
            {
                return HttpNotFound();
            }

            JournalViewModel jvm = new JournalViewModel
            {
                Id = journal.Id,
                PatientId = journal.Patient.Id,
                DoctorId = journal.Doctor.Id,
                Diagnosis = journal.Diagnosis,
                DateVisit = journal.DateVisit
            };

            ViewBag.Patients = db.Patients.ToList();
            ViewBag.Doctors = db.Doctors.ToList();

            return View(jvm);
        }

        // POST: Journals/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, PatientId,DoctorId,Diagnosis,DateVisit")] JournalViewModel jvm)
        {
            if (ModelState.IsValid)
            {
                Journal journal = db.Journals.Find(jvm.Id);
                db.Entry(journal).State = EntityState.Modified;
                Patient patient = db.Patients.Find(jvm.PatientId);
                Doctor doctor = db.Doctors.Find(jvm.DoctorId);
                journal.Patient = patient;
                journal.Doctor = doctor;
                journal.Diagnosis = jvm.Diagnosis;
                journal.DateVisit = jvm.DateVisit;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jvm);
        }

        // GET: Journals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journal journal = db.Journals.Find(id);
            if (journal == null)
            {
                return HttpNotFound();
            }
            return View(journal);
        }

        // POST: Journals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Journal journal = db.Journals.Find(id);
            db.Journals.Remove(journal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpGet]
        public ActionResult JournalDatesForm()
        {
            ViewBag.Date1 = "01.01.2020 00:00:00";
            ViewBag.Date2 = "01.01.2020 00:00:00";
            return View();
        }

        [HttpPost]
        public ActionResult JournalDatesForm(DateTime date1, DateTime date2 )
        {
            ViewBag.Date1 = date1;
            ViewBag.Date2 = date2;
            return RedirectToAction("JournalDatesForm", "Journals", new { Date1 = date1, Date2 = date2 });
        }

    }
}

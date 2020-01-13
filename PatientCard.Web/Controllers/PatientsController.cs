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
    public class PatientsController : Controller
    {
        private PatientCardContext db = new PatientCardContext();

        // GET: Patients
        public ActionResult Index(string iin, string name)
        {
            ViewBag.Iin = iin;
            ViewBag.Name = name;
            if (ViewBag.Iin != null)
            {
                return View(db.Patients.Where(a => a.Iin == iin).ToList());
            }
            else if(ViewBag.Name != null)
            {
                return View(db.Patients.Where(a => a.Name == name).ToList());
            }
            else
            {
                return View(db.Patients.ToList());
            }
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            var jvm = new JournalViewModel
            {
                Patient = new PatientViewModel
                {
                    Id = patient.Id,
                    Iin = patient.Iin,
                    Name = patient.Name
                },
            };
            ViewBag.Journals = db.Journals.Where(j=> j.Patient.Id == patient.Id).ToList();
            return View(jvm);
        }

        // GET: Patients/Create
        public ActionResult Signup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            var jvm = new JournalViewModel
                { Patient = new PatientViewModel
                    {
                        Id = patient.Id,
                        Iin = patient.Iin,
                        Name = patient.Name
                    },
                };
            ViewBag.Doctors = db.Doctors.ToList();
            return View(jvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(JournalViewModel journalViewModel)
        {
            if (ModelState.IsValid)
            {
                Patient patient = db.Patients.Find(journalViewModel.Patient.Id);
                Doctor doctor = db.Doctors.Find(journalViewModel.DoctorId);

                var newJournal = new Journal()
                {
                    Patient = patient,
                    Doctor = doctor,
                    Diagnosis = journalViewModel.Diagnosis,
                    DateVisit = journalViewModel.DateVisit
                };

                db.Journals.Add(newJournal);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(journalViewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Iin")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Iin")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
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
        public ActionResult PatientsIinForm()
        {
            ViewBag.Iin = "";
            return View();
        }

        [HttpPost]
        public ActionResult PatientsIinForm(string iin)
        {
            ViewBag.Iin = iin;
            return RedirectToAction("PatientsIinForm", "Patients", new { Iin = iin });
        }

        [HttpGet]
        public ActionResult PatientsNameForm()
        {
            ViewBag.Name = "";
            return View();
        }

        [HttpPost]
        public ActionResult PatientsNameForm(string name)
        {
            ViewBag.Name = name;
            return RedirectToAction("PatientsNameForm", "Patients", new { Name = name });
        }

    }
}

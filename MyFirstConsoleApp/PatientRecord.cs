using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstConsoleApp
{
    internal class PatientRecord
    {
        public string PatientId { get; private set; }
        public string PatientName { get; private set; }
        public string MedicalHistory { get; private set; }
        public double HeartRate { get; private set; }
        public double BloodPressure { get; private set; }
        public string BillingStatus { get;  set; }

        public PatientRecord(string patientId, string patientName, string medicalHistory, double heartRate, double bloodPressure)
        {
            PatientId = patientId;
            PatientName = patientName;
            MedicalHistory = medicalHistory;
            HeartRate = heartRate;
            BloodPressure = bloodPressure;
            BillingStatus = "IN PROCESS";
            
        }


    }
}

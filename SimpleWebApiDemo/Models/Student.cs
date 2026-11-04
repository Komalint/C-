using System;

namespace SimpleWebApiDemo.Models
{
    // =====================================================================================
    // Model: Student.cs
    // =====================================================================================
    // WHAT IS A MODEL?
    // In Web API / MVC, a "Model" is a plain C# class (often called a POCO - Plain Old C#
    // Object) that defines the structure and shape of your data.
    //
    // When a client (like a mobile app, browser, or Postman) sends JSON data:
    // {
    //     "name": "Komal",
    //     "course": "Computer Science",
    //     "age": 21
    // }
    // ASP.NET Web API automatically converts that JSON into this C# Student object!
    // This magic is called "Model Binding".
    //
    // Similarly, when you return a Student object in C#, Web API automatically converts
    // it into JSON text for the client. This is called "Serialization".
    // =====================================================================================
    public class Student
    {
        // Unique identifier for each student (e.g., 1, 2, 3...)
        public int Id { get; set; }

        // The student's name (e.g., "Komal Gupta")
        public string Name { get; set; }

        // The course or subject they are studying (e.g., "Full-Stack Development")
        public string Course { get; set; }

        // The student's age (e.g., 22)
        public int Age { get; set; }
    }
}

# Student Management System Website
In this website, an administrator will add students to use a virtual portal, and can then edit student details and delete students as well. Students can then make their own accounts
if there are details stored for them and can then select different modules to register to do, and can remove modules that they don't want to do anymore. There are checks in place to
ensure users can't select the same module twice or do more than five modules in a semester. The administrator can view each student's module choices and can remove any if needed.
There is also security in the website to make sure students can't access admin pages and so anonymous users can't register new modules or students.

The student, account and module details are all stored in a SQLite database that is hosted on a separate backend server, and the code for this server can be found here: https://github.com/frhn13/StudentManagementSystemDatabase
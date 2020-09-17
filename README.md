# UMT.Transport
Umit transport, Bilthoven

CURRENT CODE COUNT:
===================
      3654
===================

====26-8-2020====
Added:

SQLite handler updated
- can now see if person is Courier, Depot employee or sort employee. based on the selected work it will let you see the new days or selected days of that kind of work.

Usercontrols:
1. Choose Depot
2. Choose Work
3. Choose options to do for the selected work.

Also SqliteDB has been updated with new forgein keys and constrains same as Unqiue bindings.

=================

====16-8-2020====
Added:

PersonModal class now has Start time and year.

SqliteHandler updated:

Load all employees on name, if there are more employees with the same name only show one firstname and show multiple lastnames in combobox lastname from LoadAllEmployeesOnLastName METHOD and LoadAllEmployeesOnName.

LoadEmployeeId from db using the lastname and place in textfield.

SaveNewEmployeeWorkDay with Date, Year, Start_time, PersId.
Saving new workday is also updated:
One person can only work on a day with different start times, so no duplicates.

=================


====15-8-2020====
Added:

PersonModal "Employee data" class.
Sqlitehandler "LoadEmployees and auto fill from sqlite" class.

Page:
NewWeekPlanning "Here you can add new data to the selected calendar day" or see on what day people need to work.

Added SQLite db reference to bin. and connectionstring added to the app.config

=================

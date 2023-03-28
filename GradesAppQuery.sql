
CREATE TABLE users (
    username varchar(45) primary key,
    password varchar(100),
    role varchar(45)
);
CREATE TABLE students(
	RegistrationNumber INT primary key,
	Name varchar(45),
	Surname varchar(45),
	Department varchar(45),
	USERS_username varchar(45) FOREIGN KEY REFERENCES users(username)
);
CREATE TABLE professors(
	AFM INT primary key,
	Name varchar(45),
	Surname varchar(45),
	Department varchar(45),
	USERS_username varchar(45) FOREIGN KEY REFERENCES users(username)
);
CREATE TABLE secretaries(
	Phonenumber INT primary key,
	Name varchar(45) NOT NULL,
	Surname varchar(45) NOT NULL,
	Department varchar(45) NOT NULL,
	USERS_username varchar(45) FOREIGN KEY REFERENCES users(username)
);
CREATE TABLE course(
	idCOURSE INT primary key,
	CourseTitle varchar(60),
	CourseSemester varchar(60),
	PROFESSORS_AFM INT FOREIGN KEY REFERENCES professors(AFM)
);
CREATE TABLE course_has_students(
	id_course_has_students INT primary key,
	COURSE_idCOURSE INT FOREIGN KEY REFERENCES course(idCOURSE),
	STUDENTS_RegistrationNumber INT FOREIGN KEY REFERENCES students(RegistrationNumber),
	GradeCourseStudent INT
);
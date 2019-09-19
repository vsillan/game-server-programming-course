# Game Server programming with ASP.NET Core and MongoDB

This is a course about game server programming made for Metropolia UAS Game Development curriculum. The aim of the course is to give an overview of creating Web API's suitable for creating online features typical to games. This course is not about creating realtime multiplayer games.

The technologies chosen for this course are ASP.NET Core and MongoDB. The main reasons for choosing these particular technologies are that they are open source and capable of handling demands of global scale online games.

## Requirements

- Basic object oriented programming skills (C# is not a prerequisite)

## The weekly schedule

1. Intro to game server programming; C# for server development (part 1)
2. C# for server development (part 2)
3. ASP.NET Core Web API (part 1); REST-architecture
4. ASP.NET Core Web API (part 2);
5. Intro to NoSQL databases; MongoDB (part 1)
6. MongoDB (part 2)
7. Project work
8. Student presentations

## Way of working

Read the articles from the reading lists before the lectures where the concepts are presented.

Try to complete the assignments on the same week they are presented. _Assignments need to be completed in order because they build on top of each other!_ By not completing the assignments in time, you make it harder for yourself to make progress in the course.


Assignment solutions should be put inside your own GitHub (or similar) repository where I can have access to them.

The project assignment will be presented on week 2 and it's recommended to start working on it as soon as possible after that.

## Tools used in the course

- VSCode: https://code.visualstudio.com/download
- .Net core SDK: https://www.microsoft.com/net/download
- Omnisharp plugin for VSCode called `C#`: https://github.com/OmniSharp/omnisharp-vscode (you can download this inside VSCode from the extensions tab! Search for "C#".)
- `C# Extensions` plugin for VSCode (you can download this from VSCode as well. Search for `C# extendsions`).
- MongoDb: https://www.mongodb.com/download-center/community
- PostMan 2: https://www.getpostman.com/apps

If you are running Windows and your VSCode uses `powershell` in the terminal, I recommend that you change it to `CMD`.

### Tips for using PostMan

How to make a simple request:
  - Add API url to the address bar (for example: http://localhost:5000/api/players)
  - Select a suitable Http verb (for example: GET)
  - When you want to add a request body: select the "body" tab -> choose "raw" -> change the format from "Text" to "JSON" -> add JSON to the body (for example: { "name" : "test" })

## Grading

The course grading scheme is straightforward:

There is a total of 100 points to be gained from the course.

- 50 points come from the assignments (points are gained by completing the assignments)
- The other 50 comes from the project (points reflect the quality of the project)

Points are rounded to the closest 10. After rounding the points are converted to the final grades which are numbers from 0 to 5.

- 50 or less equals 0 (course failed)
- 60 points equals 1
- 70 points equals 2
- 80 points equals 3
- 90 points equals 4
- 100 points equals 5

You can also affect your grade positively by participating actively in the lectures.

## Deadlines

The deadlines for the assignments are the following:

- Project: 10.10 2019
- All assignments: 10.10 2019

## Completing the course without class room participation

It's highly recommended to participate in the lectures because the concepts taught in the course have been generally hard for students to grasp - usually being able to ask questions and get personal support ensures success in the course. In addition to support, being present in the lectures gives you more chances to demonstrate your skills and affect your grade positively.

But if you nevertheless can not participate in the lectures, the course can be completed by doing the assignments and the project within the deadlines.

Remember to read the reading materials and slides through carefully, the information found in those should be enough to complete the exercises.

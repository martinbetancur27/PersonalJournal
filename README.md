# ASP.Net Core Web App (MVC) with Razor Pages

## If you're short on time, AuthJWTProject is my best practice project.
---
# Tech Stack

**Client:** Html, Bootstrap 5, C# (Razor)

**Server:** C#, EF, SQL SERVER, Azure

```bash
N-Layer Architecture
```
# Try App 👩‍💻
## Sorry, the resources have been deleted. The video shows how it works.
https://personaljournalapp.azurewebsites.net  DONT WORK


# Watch video of how it works 🎥

https://www.dropbox.com/s/j9vyuogxun36x1u/PersonalJournal.mp4?dl=0

# 🛠 Skills
* C#
* Razor Pages
* Html with Bootstrap 5
* Separation of responsibilities in the code and architecture
* Implementation of Interfaces and Dependency Injection
* ORM > Entity Framework Core with SQLSERVER
* Code First
* Validate data input from the Backend and from the frontend
* Identity > Authentication
* Set up a mail server - Gmail
* Deploy to Azure from visual studio

## Warnings

* The controller has logic that should be placed in the service layer. In the other two .Net projects the principle is fulfilled.
* User registration: emails should be confirmed. I am not attaching the functionality so as not to waste time for users testing the application. Also, do not configure to require a strong password to make testing easier.
* To not include passwords in the code you can use Azure key vault.

## Pending improvements

* Allow only 12 journals per user and 15 notes for each journal
* Add authentication with Google.
* Implement Unit Of Work.
* Place the generic repository pattern. I had previously implemented it but removed it for fear of losing code readability and increasing coupling. I conclude that the generic does not increase coupling or decrease cohesion because it is on the same EF library. Meet DRY.
* Create extension method to separate dependency injection configuration; another to separate the connection configuration to the database.

# UML

![My Remote Image](https://ucd7d4f679ccb60f3d885b25757a.previews.dropboxusercontent.com/p/thumb/ABzhLfH5hS9HN61yfM9j3lbY1hS5PryrKXRM0PuJUYqglmPJ9nwkqpT3BdAqpnqZIJY0muNr6l82aIRsSwJ-JTqfWANsW5nLsdou8NqAW82c59nVv2HiaVuFHLKl8bORRLl-2HxutR4PhFu8AnXEcmR8GFalbZaPD8UMoZI56TFyzpZqgtdxjSLNuMFAfvN561kxbTZbZicYGM40Anr1m-6R_0aC1WuR70Cs5FkqLHyIuhW7sa6v_rHtTA-4UBpPjAsYgb5NCyrx3CyMuU-XGXx58jnvU-MqMJlNxhzP1iA2StJegljEtq5yXX3Q9N2Q7yyKTFfhR9z3bCqNQoRl1IRHspSkw1m2xRqHiYvzMdjo-5UTk7akCkOwDg8Kf6khqmwYKi603i735UJXHOLClhOJdz8T6GQa7knwArXrkvqIvA/p.png)

# DB: Physical diagram

![My Remote Image](https://uc4cd59f6c8edf509b03c56ec526.previews.dropboxusercontent.com/p/thumb/ABzyE5LvHK3J37wzXrYtJKOoToicaqM6hacF6dxL3kX1EgqSy1Mw2wCOs634lEiInP_pk_pxnbDIpj5_Ztb6kraaVklBfAOWVvCmClbb3snnCC2fHyLofHvym4weNiLk-xx7Z251BO14beM11wc9oI_3Fh58jAYqikjlScBFCA-hRbYu9Q5Hn1H65luoynQEk75dao1Kyse94NUSI2AAlm_O7orAkWVmdflHlTWkPIwbvAwBxOjvY1HJxMP9VgrjPIN46KjkT4iqymro07KmJjPhWhNtcfro6xQHKb3vOoPk71Zj47aYFTXcIHkegq9gfL3MI7wjYchpbaIc7w9XKpEwVQjAZoR7fNqBAkNrNztjTVtVnF7G_Wg1YbQEax81GR_d2j3TD8cKDFbuJcXEw9eEZAbevvIdJfb5MeHLkcRWfA/p.png)

### Note:
    From the frontend and the backend the size that can be entered into the database is limited. Later the sizes will be adjusted in the db from Fluent API

### Courses

![Fundamentos .NEt](https://uca2ef9a28004d7dcebb25761b94.previews.dropboxusercontent.com/p/pdf_img/ABwevM_VNdCoNAN0PbP7S4BLo4nq0dB03x5OsVyXjTwdwHADLst_VF7tUT7UfDLkhPmY85PEoXw2JLThpNRuGBJVoxhnUmvej8aE7CEMvPuXjQVif5Fsugqz5puDC3z7eta9X74XqWBJCC4Vsz4Eq6Zs2aSq22vgHdSdRikw5nJxq8lVChDp0Fq6Wdi5bRNt0HEtFGyK5Qmnw_MGxMbRltSopbF9wyc1gceMgDoIS0mIO1mNqmoJhqXkzzKFvVzShAV4tJcqsiyevPUrD2Fx4TiUNq2xE5cPHJSSuD7V5MX9E4u6241Hh7r0raq0zoKh4coNdXgPOH4lta5YwxNlxOf-NXcPHy7_BfQNc8YvuxFNv5JD5Cao7OlbXjDKTcqQdnmFKg3Zty4OSlzBEeahsL13/p.png?page=0&scale_percent=0)

![Entity Framework](https://ucb3e5c2be55249332d86fbcf259.previews.dropboxusercontent.com/p/pdf_img/ABtAeHr0IB3SZ4t11wW30icDm0-c1fon2O4MtrszevzpMVxuEelNNIWG6FyEkXqm4hIakr8D8FJbBhQOV0M_f6nuRNFluRHPlQ1h7Mo4nTmDpxwz_37kxja3Lr01_q_CSINs-DMlBUI4MuJya6qFO0YG51mwJp08nidKpInkAxNsLE3qqJHpfF8SILboo8LYLEsCjqxQrh4GFxSY1w_dtP9iN0OjfrWL-wtanUVGleBGxMJ6egFXoOt-vevMzzo1f20yTOPWdclzamqzqq0hmjzVMLHKmNtkilbzkTFnRiJuVIU_zywRsVWhpi_Gigy7UGUGgrhLqK_iwEeDteRhxmA4WyKVaSg4SLo6rJPOb9tEUf_vs0Yh9kwviBlr1lrE3VOXt1BB3Q288tRMdVw9qEYU/p.png?page=0&scale_percent=0)

![Fundamentos base de datos](https://uc995d8361d579c970c01b6750e4.previews.dropboxusercontent.com/p/pdf_img/AByZkdcV9523F5qYu8Fa_Ce4kWJoEwA4eOm97ZCDZfhlvlxRk6TVSiQOnSditr7twEzDhmf38SNZBJaQAJETyIAiPMFF6LkIz0GEkUvEhZhZ2HzT_o1cHk0133i6La6xc55WRNMfA60GEUk-8nDR1s1635glD0WWcwHMoZoQ6hFRJOMyZW3tvGoEnXWNdWnhwv8NrNiMBDrqDISR8N-u123x7WISwb_0wguGW_C8g95dqYW_wrUkDgJDyoHPGBhOHFtuEYSN-bSF-UBlTGdRZ-oJbmNgtOmHKu4Si4dp6BWspmBZEZJLAVSuxLgnpAAY7HVHEnS5_iF7TWOboh50tdf2n3c5JVMBw_fC_qKDz-qAkbdHNFVswy_iHkn9e_vB15lHg_d4-M0bhY5Dv4WGSwN0/p.png?page=0&scale_percent=0)

![C# - Buenas prácticas y código limpio](https://uc9d6dd216beadf871447b22f666.previews.dropboxusercontent.com/p/pdf_img/ABuxzKUG0NqgwTHbJCWV-05mSG8dN7z1gs2OAV_Fc9FS3FvJyrp-JqOV-UQdCAEGyKTBb7PBPRYMICMHVWquIYgGH5DTdWdpRbxip2bAePV7Aoe1cqG1p_pUuXHP2Ml1TUlRWYS9ruWV-TSNQQQ-UFfQY8VMlASpSuHER4MlDNuTJafgfA8z82mttBihwfTIzE5FE0E00rsBf3fHe1oZ9IBpr3fH0XE1RxDA0KMGoBZ2u9Cr_tpyZCJF_F7vEgzGaGwYGAA38GiAudfz6J4_9A9o-p3MXsCjvveEtSs0mluUW_W-9-Na3fx8VEgGpcHGcWQzpgXn36dAVsN4y08yj_ixts_fgV3jfvpPzud1d195-pvYLBPt1piqq1w9qQXTcqo2Q--Wvv1D3W65XNuIKMyz/p.png?page=0&scale_percent=0)
# ASP.Net Core Web App (MVC) with Razor Pages

# Tech Stack

**Client:** Html, Bootstrap 5, C# (Razor)

**Server:** C#, EF, SQL SERVER, Azure

```bash
N-Layer Architecture
```
# Try App 👩‍💻

https://personaljournalapp.azurewebsites.net

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

* User registration: emails should be confirmed. I am not attaching the functionality so as not to waste time for users testing the application.
* To not include passwords in the code you can use Azure key vault

## Pending improvements

* Add authentication with Google.
* Add pagination
* Allow only 12 journals and 15 notes for each journal

# UML

![My Remote Image](https://ucd7d4f679ccb60f3d885b25757a.previews.dropboxusercontent.com/p/thumb/ABvoHb5VVLn4ItJ572wVW0qFl8CMyn85lsSUwjwn-vkGXzvGtmU7Mc3XSVkEvdYsSTd7mgyT4hsNN3AjPcygoKjYDS5oP8k5g0W6WkWefmFQBOCv7l5Tyx1PSAQqivwLE8LXKZVdA4B0Gy37HXOrSor2If6YJ6EHui4Edtrigbk_V0tavvcrd4MbpHtpX2PdZ0T8qO0hiNi2igZ3s8sMOgQ9KDQvK2tcT_tTPr2wG2oZpkdzdO1emU53oDBImZ4XNlZBdd0FKr-zqwO0I_OXTZk-s5XghVUqi384SnAOwuHYzEU8MRyAFNEUZsBM5TnqxVkhoAACA8gzfm_oS2pVVqTNGMcsN1fq4OKpHWO7mC8xzZ5J1dAb6MDU7Jf8Vn6CIj-tS2LSlGdX7wjgEnNEKcoiF1978vkEdu67pJlsmENNqg/p.png)

# DB: Physical diagram

![My Remote Image](https://uc4cd59f6c8edf509b03c56ec526.previews.dropboxusercontent.com/p/thumb/ABtJAI1ZFNp2INoEi9vM9ruxJFun5IE0hHkpmWtr7fi91W3gBcAVBRVWZVs5pC9ZCV5Bk932WVXId0H30RBmv-XJrMyp8t0yxavc1oNRqMoziIu94EuUP91s8qe4SMgKI_MJgVQlvS5MdRUjs1kTVq0AJQlxUalQNBFLlGoZajmomtyXUPVmRnolOPzitkMBjtUKA7b3ZS22mogiXlyclf3eaJazY-ITn2TVtdS94KzGtaM1evsgqgXEs_Dh5bYhp77acDyJH3FnYsEUr74N5PTVR3b7Q3jxDsD6wnd_H689HmqVsQ0qufIeHT4-1-s32WRGI67Bg_E7aTbYXYUqK7Lqpj8fPf_TwIrZodAzDjVgjUgqjm3TlAjXV3Bui4Hi17qWD9Dy0EFe3SxVJf2S3AwQUh68W7zeJx41icevlyQWXg/p.png)

### Note:
    From the frontend and the backend the size that can be entered into the database is limited. Later the sizes will be adjusted in the db from Fluent API

### Courses

![Fundamentos .NEt](https://uca2ef9a28004d7dcebb25761b94.previews.dropboxusercontent.com/p/pdf_img/ABs4M8tt3N2o-Y8blAP6pd6hI_7Z6lcMo6b-hqdWHXKJ3YA_guJhRN-JX3fIB8zfDR15VL35y1zK1BRcUCidPmrihXx_JEFoKlboF_yYWdK1buPVMa4ztWr9KnAV34ynkn6SpfqQXgwRfxHntnJDWZNsrQFqT_HR8v5eiiGNgsh4cmqrzzsZDwJNSPDGvb3xKIn6RZZ1JfoRNZjG8aC4YBIYFonmmZsWNSz2VmE3nGcr-Dow1yz8oaB-1bXQIeVp5qtMw7bIIn73MM1XIkCK-2vW3lEUQU3CNhevKK1cVf7mhliBvnExD6QrHGaiobKDbsaf0V68nReukkkIy1HiukVmAMYpSco3dbk8QGeYIVi997-WphpPZmk1ve0rj2g0swvVFRteP8MXqBoozDiaKi21/p.png?page=0&scale_percent=0)

![Entity Framework](https://ucb3e5c2be55249332d86fbcf259.previews.dropboxusercontent.com/p/pdf_img/ABtAeHr0IB3SZ4t11wW30icDm0-c1fon2O4MtrszevzpMVxuEelNNIWG6FyEkXqm4hIakr8D8FJbBhQOV0M_f6nuRNFluRHPlQ1h7Mo4nTmDpxwz_37kxja3Lr01_q_CSINs-DMlBUI4MuJya6qFO0YG51mwJp08nidKpInkAxNsLE3qqJHpfF8SILboo8LYLEsCjqxQrh4GFxSY1w_dtP9iN0OjfrWL-wtanUVGleBGxMJ6egFXoOt-vevMzzo1f20yTOPWdclzamqzqq0hmjzVMLHKmNtkilbzkTFnRiJuVIU_zywRsVWhpi_Gigy7UGUGgrhLqK_iwEeDteRhxmA4WyKVaSg4SLo6rJPOb9tEUf_vs0Yh9kwviBlr1lrE3VOXt1BB3Q288tRMdVw9qEYU/p.png?page=0&scale_percent=0)

![Fundamentos base de datos](https://uc995d8361d579c970c01b6750e4.previews.dropboxusercontent.com/p/pdf_img/ABvfQK3g-Mj6de6Ex6IoqxHx3D9BtJXnKPwNSXhwT2jbgNyV_GmVvZSjbO73XnxTj1GXz4uEOEUHV4qRoPmZpsYBPgvHr_IVLlR8tjn4vm1-red_ifBApWlnUOvsd6eAvFlbxRA9jFC2CCEf25qTExVBEC6IfqHOXXtJBxKL8ibVOjygCQUYE1i5TI7-QVamrzZpWXGGfFbEmsg18utcx-_wO8A_0mCy5lPNvTokykFeZRQUxeOvI3d4lmos3r_WqGJFDXy15tIjwCdQi63WVf5dn_wQSdSN3soFx_X_luQEse_tNBToSBSVKzTaqD6YQ_KghaHJDZ2p_3o_WLtt-DedZ8-4B8Xc_EdOIs7_HNjipFntms5EfUl7IOy1vzDfEc1gDJqjBClcL81IHYQiWZkZ/p.png?page=0&scale_percent=0)

![C# - Buenas prácticas y código limpio](https://uc9d6dd216beadf871447b22f666.previews.dropboxusercontent.com/p/pdf_img/ABuxzKUG0NqgwTHbJCWV-05mSG8dN7z1gs2OAV_Fc9FS3FvJyrp-JqOV-UQdCAEGyKTBb7PBPRYMICMHVWquIYgGH5DTdWdpRbxip2bAePV7Aoe1cqG1p_pUuXHP2Ml1TUlRWYS9ruWV-TSNQQQ-UFfQY8VMlASpSuHER4MlDNuTJafgfA8z82mttBihwfTIzE5FE0E00rsBf3fHe1oZ9IBpr3fH0XE1RxDA0KMGoBZ2u9Cr_tpyZCJF_F7vEgzGaGwYGAA38GiAudfz6J4_9A9o-p3MXsCjvveEtSs0mluUW_W-9-Na3fx8VEgGpcHGcWQzpgXn36dAVsN4y08yj_ixts_fgV3jfvpPzud1d195-pvYLBPt1piqq1w9qQXTcqo2Q--Wvv1D3W65XNuIKMyz/p.png?page=0&scale_percent=0)
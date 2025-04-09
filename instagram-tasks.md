### 20 дополнительных заданий для Instagram API

#### **UsersController** — Пользователи
1. **GET /users/new-registrations**  
   - Описание: Возвращает пользователей, зарегистрированных за последние 14 дней (добавьте поле `JoinDate`).  
   - DTO: `NewRegistrationDto`  
     - `Username` (string)  
     - `Email` (string)  
     - `JoinDate` (DateTime)  

2. **GET /users/active-posters**  
   - Описание: Показывает пользователей с хотя бы одним постом.  
   - DTO: `ActivePosterDto`  
     - `Username` (string)  
     - `PostCount` (int)  

3. **GET /users/recently-active**  
   - Описание: Возвращает пользователей с количеством постов за последние 7 дней.  
   - DTO: `RecentlyActiveUserDto`  
     - `Username` (string)  
     - `LastPostDate` (DateTime)  
     - `PostCount` (int)  

4. **GET /users/top-creators**  
   - Описание: Показывает 5 пользователей с наибольшим числом постов.  
   - DTO: `TopCreatorDto`  
     - `Username` (string)  
     - `PostCount` (int)  

5. **GET /users/high-interaction**  
   - Описание: Возвращает пользователей, чьи посты получили в среднем больше 5 комментариев.  
   - DTO: `HighInteractionUserDto`  
     - `Username` (string)  
     - `PostCount` (int)  
     - `AvgCommentsPerPost` (double)  

#### **PostsController** — Посты
6. **GET /posts/latest-posts**  
   - Описание: Возвращает 5 самых новых постов.  
   - DTO: `LatestPostDto`  
     - `Content` (string)  
     - `Username` (string)  
     - `CreatedAt` (DateTime)  

7. **GET /posts/user-recent**  
   - Описание: Показывает последние 5 постов пользователя по его ID.  
   - DTO: `UserRecentPostDto`  
     - `Content` (string)  
     - `CreatedAt` (DateTime)  
     - `Username` (string)  

8. **GET /posts/high-comment**  
   - Описание: Находит посты с более чем 10 комментариями.  
   - DTO: `HighCommentPostDto`  
     - `Content` (string)  
     - `Username` (string)  
     - `CommentCount` (int)  

#### **CommentsController** — Комментарии
9. **GET /comments/recent**  
    - Описание: Возвращает 5 последних комментариев.  
    - DTO: `RecentCommentDto`  
      - `Text` (string)  
      - `Username` (string)  
      - `CreatedAt` (DateTime)  

10. **GET /comments/by-post-id**  
    - Описание: Показывает последние 5 комментариев к посту по его ID.  
    - DTO: `PostRecentCommentsDto`  
      - `Text` (string)  
      - `Username` (string)  
      - `CreatedAt` (DateTime)  

11. **GET /comments/long-text**  
    - Описание: Находит комментарии длиннее 200 символов.  
    - DTO: `LongTextCommentDto`  
      - `Text` (string)  
      - `Username` (string)  
      - `TextLength` (int)  

12. **GET /comments/quick-responses**  
    - Описание: Показывает комментарии, оставленные в течение 15 минут после поста.  
    - DTO: `QuickResponseCommentDto`  
      - `Text` (string)    
      - `Username` (string)  
      - `PostId` (int)  
      - `TimeDifference` (TimeSpan)  

#### **Смешанные запросы** — Комбинированные
13. **GET /users/{id}/activity-summary**  
    - Описание: Показывает последние 3 поста и 3 комментария пользователя.  
    - DTO: `ActivitySummaryDto`  
      - `Username` (string)  
      - `Posts` (List: `Content`, `CreatedAt`)  
      - `Comments` (List: `Text`, `PostId`)  

14. **GET /posts/recent-popular**  
    - Описание: Возвращает 5 последних постов с более чем 5 комментариями.  
    - DTO: `RecentPopularPostDto`  
      - `Content` (string)   
      - `Username` (string)  
      - `CreatedAt` (DateTime)  
      - `CommentCount` (int)  

15. **GET /users/top-commenters**  
    - Описание: Показывает 5 пользователей с наибольшим числом комментариев.  
    - DTO: `TopCommenterDto`  
      - `Username` (string)   
      - `CommentCount` (int)  

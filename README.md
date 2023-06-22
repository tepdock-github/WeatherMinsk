# WeatherMinsk
WeatherMinsk - web-приложение, позволяющие получать и создавать посты о погоде.
Основной стек: ASP.NET Core, ADO.NET, SQLite, React + ts, Tailwind
Для тестирования: xUnit, Moq, Swagger

Описание endpoint-ов:
1. GET: /api/v1/posts - получение постов с базы данных, при их отсутсвии получаем и сохраняем пост с WeatherAPI
2. GET: /api/v1/posts/{id} - получение поста по id
3. POST: /api/v1/posts - создание поста

Инструкция по запуску:
Открыть консоль и выполнить команду: docker-compose up. Далее перейти по адресу: http://localhost:4173/
Для тестирования API с помощью swagger - http://localhost:5023/swagger/index.html
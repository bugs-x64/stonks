# STONKS API

![картинка](https://i.kym-cdn.com/entries/icons/original/000/029/959/Screen_Shot_2019-06-05_at_1.26.32_PM.jpg)

Инструкция по использованию:
* Собрать проект
* Запустить проект
* Если установлен [**docker**](https://docs.docker.com/compose/install/), проект можно собрать и запустить с помощью команды 
  ```bash
  docker-compose up --build
  ```
* Перед использованием методов апи необходимо выполнить запрос 

```bash
curl -X 'POST' \
  'https://localhost:5001/api/Tools/FillData' \
  -H 'accept: */*' \
  -d ''
```

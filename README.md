# ASE.PodISMConsole

**Данное приложение было разработанно для компании. 
Задача заключалась в написании скриптов и тестов. 
Приложение конвертирует Json-файл в CSV формат и из CSV файла в Json формат.** 
 
## Aim of the Project

- Особенность проекта в том, что он был направлен на оттачивание знаний языка программирования C#

- Научиться делать работать с модульными тестами, в данном случае мы использовали платформу Nunit

- Разработанный код был написан и воспроизведен без использования библиотек

## About of the script

Все данные должны находиться в папке data с правильным форматом

1. Для первого скрипта используется формат Json-файла такого вида:

```json
{
     "processes": [
    {
      "UID": "1",
      "Title": "",
      "Link": "",
      "Chields": [
        {
          "UID": "1.1",
          "UpUID": "1",
          "Title": "",
          "EmpParentProcess": "",
          "EmpDevBy": "",
          "GeneralInfoName": "",
          "DistributionArea": "",
          "JustificationOrder": "",
          "LinkProcessMap": "",
          "Link": "",
          "Chields": [
            {
              "UID": "1.1.1",
              "UpUID": "1.1",
              "Title": "",
              "EmpParentProcess": "",
              "Chields": []
            },
            ...
        }
    }
}
```

В итоге получается файл pm.content.ver37-main.csv


2. Во 2 скрипте мы получаем json-файл из двух csv-файлов

```json 
{
  "processes": [
    {
      "UID": "1",
      "UpUID": null,
      "Title": "",
      "EmpParentProcess": "",
      "EmployeeParentProcess": [
        {
          "Id": "1",
          "FullName": "",
          "Name": "",
          "Surname": "",
          "Patronymic": "",
          "ServiceNumber": "",
          "Email": "",
          "Position": "",
          "Subdivision": "",
          "Organization": ""
        }
      ],
      "EmpDevBy": "",
      "EmployeeDevBy": [
        {
          "Id": "2",
          "FullName": "",
          "Name": "",
          "Surname": "",
          "Patronymic": "",
          "ServiceNumber": "",
          "Email": "",
          "Position": "",
          "Subdivision": "",
          "Organization": ""
        }
      ],
      "GeneralInfoName": "",
      "DistributionArea": "",
      "JustificationOrder": "",
      "LinkProcessMap": "",
      "Link": "",
      "Chields": []
    }
  ]
}
```
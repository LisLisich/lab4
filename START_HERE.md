# Лабораторная №4 — Git и GitHub с нуля

Это исходный проект и инструкция, а не отчёт о выполненных действиях. Публикация, командная работа и запуск на Windows ещё не выполнены. Истории Git в архиве нет: вы создадите её сами. Формы создаются кодом в Program.cs, без визуального конструктора.

## 1. Подготовка и запуск

Нужен компьютер с Windows. В Visual Studio Installer включите «Разработка классических приложений .NET» и targeting pack .NET Framework 4.7.2. Проект рассчитан на Visual Studio 2019. Если среда предлагает установить целевой пакет, установите его. Visual Studio Code — другая программа.
Установите Git for Windows: https://git-scm.com/downloads .
Распакуйте архив, например в C:\Projects. Откройте Lab4Git.sln. Нажмите Ctrl+Shift+B, затем F5. Проверьте: пустое имя вызывает предупреждение; введённое имя отображается во втором окне; кнопка «Назад» и крестик второго окна возвращают первое окно. Остановите программу перед изменением веток.
Начиная с VS 2019 16.8 Git встроен. Старое расширение GitHub Extension из методички для этих версий не требуется. Команды ниже вводятся в Git Bash; просмотр истории можно выполнять и средствами Visual Studio.

## 2. Первые коммиты

Откройте Git Bash в папке, где лежит Lab4Git.sln (не во вложенной папке с Program.cs). Подставьте своё имя и email; можно использовать noreply-email из настроек GitHub. Настройки ниже относятся только к этому проекту.

```bash
git init
git branch -M master
git config user.name "Ваше имя"
git config user.email "Ваш email"
git add .gitignore
git commit -m "Добавлены фильтры временных файлов"
git add Lab4Git.sln Lab4Git START_HERE.md
git commit -m "Добавлена программа с двумя формами и проверкой имени"
git status
git log --oneline
```

master выбрана по методичке; main выполняет ту же роль. .gitignore исключает служебную папку .vs, результаты сборки bin/obj и личные настройки. Исходники .cs, .csproj и .sln сохраняются. Проверить фильтр: `git check-ignore -v Lab4Git/bin/Debug/Lab4Git.exe`.

## 3. Новая функция в отдельной ветке

```bash
git checkout -b Addfunction
```

В Lab4Git/Program.cs замените строку `// ADD_COLOR_BUTTON_HERE` этим кодом:

```csharp
var color = new Button { Text = "Сменить цвет", Location = new Point(190, 150), Size = new Size(200, 40) };
color.Click += (sender, e) =>
{
    BackColor = BackColor == Color.LightBlue ? SystemColors.Control : Color.LightBlue;
};
Controls.Add(color);
```

Запустите программу, проверьте смену цвета второй формы. Сохраните файл, остановите программу.

```bash
git add Lab4Git/Program.cs
git commit -m "Добавлена смена цвета второй формы"
```

Измените текст кнопки с «Сменить цвет» на «Изменить фон», сохраните файл.

```bash
git add Lab4Git/Program.cs
git commit -m "Уточнено название кнопки смены фона"
git checkout master
```

В master кнопки ещё нет. Запустите и проверьте, затем остановите программу. Сравните ветки и выполните слияние:

```bash
git diff master..Addfunction -- Lab4Git/Program.cs
git merge --no-ff Addfunction -m "Объединена ветка смены фона с основной"
git log --graph --oneline --all
```

Теперь кнопка есть и в master. Новые коммиты в master не переносятся автоматически в Addfunction.

## 4. Учебный конфликт

Создадим конфликт в отдельном текстовом файле, чтобы не ломать программу. Выполняйте блоки по порядку в Git Bash.

```bash
printf 'Заголовок: исходный вариант\n' > conflict-demo.txt
git add conflict-demo.txt
git commit -m "Добавлен файл для учебного конфликта"
git checkout -b ConflictDemo
printf 'Заголовок: вариант дополнительной ветки\n' > conflict-demo.txt
git add conflict-demo.txt
git commit -m "Изменён заголовок в дополнительной ветке"
git checkout master
printf 'Заголовок: вариант основной ветки\n' > conflict-demo.txt
git add conflict-demo.txt
git commit -m "Изменён заголовок в основной ветке"
git merge ConflictDemo
```

Сообщение CONFLICT здесь ожидаемо. Сделайте скриншот. Откройте conflict-demo.txt: Git отметил несовместимые строки маркерами <<<<<<<, =======, >>>>>>>. Замените ВСЁ содержимое на одну строку «Заголовок: согласованный вариант», сохраните файл.

```bash
git add conflict-demo.txt
git commit -m "Разрешён конфликт заголовка"
git status
git log --graph --oneline --all
```

## 5. Публикация на GitHub

На GitHub создайте репозиторий lab4-git-demo. Если требуется строго по методичке, выберите Public. При создании НЕ добавляйте README, .gitignore и лицензию: репозиторий должен быть пустым. Замените YOUR_LOGIN своим логином:

```bash
git remote add origin https://github.com/YOUR_LOGIN/lab4-git-demo.git
git push -u origin master
git push -u origin Addfunction
git push -u origin ConflictDemo
```

При запросе авторизации войдите в GitHub в предложенном браузере. Не вставляйте обычный пароль GitHub в команду. Если авторизация не проходит, сохраните текст ошибки без секретов. Проверьте на сайте файлы, ветки и историю коммитов.

## 6. Отдельные ветки лабораторных

Для текущей работы создайте lab4, затем выполните два осмысленных изменения описания:

```bash
git checkout master
git checkout -b lab4
printf 'Лабораторная №4: Git и GitHub.\n' > LAB4.md
git add LAB4.md
git commit -m "Добавлено описание лабораторной 4"
printf '\nПроверки: ввод имени, переход, возврат, смена фона.\n' >> LAB4.md
git add LAB4.md
git commit -m "Добавлен список проверок программы"
git push -u origin lab4
```

Для КАЖДОЙ другой лабораторной, которую требует преподаватель: перейдите в master, создайте lab1, lab2 и т. д., скопируйте в неё реальный соответствующий проект (без его вложенной папки .git), сделайте коммит исходников и второй коммит содержательного улучшения или инструкции запуска, затем отправьте ветку. Не заменяйте отсутствующие работы пустыми ветками. Исходники предыдущих работ в этот архив не включены.

## 7. Клонирование чужого репозитория

В GitHub откройте разрешённый публичный репозиторий одногруппника, скопируйте HTTPS-адрес через Code. В Git Bash перейдите в C:\Projects (вне текущего репозитория):

```bash
cd /c/Projects
git clone https://github.com/OWNER/REPOSITORY.git classmate-project
cd classmate-project
git log --oneline -5
```

Замените OWNER/REPOSITORY настоящими значениями. Скриншот результата подтвердит клонирование. Чужой код не следует выдавать за свой.

## 8. Работа вдвоём

Владелец lab4-git-demo приглашает одногруппника в настройках доступа репозитория; приглашённый принимает приглашение. Не передавайте пароль.
Участник 2 клонирует общий репозиторий и создаёт ветку teammate-help. В Program.cs меняет строку заголовка второй формы с «Вторая форма» на «Приветствие пользователя», делает коммит, затем меняет подпись кнопки «Назад» на «На главную», делает второй коммит и отправляет ветку:

```bash
git checkout -b teammate-help
# Выполнить первое изменение и сохранить файл
git add Lab4Git/Program.cs
git commit -m "Уточнён заголовок окна приветствия"
# Выполнить второе изменение и сохранить файл
git add Lab4Git/Program.cs
git commit -m "Уточнена подпись кнопки возврата"
git push -u origin teammate-help
```

Владелец тем временем на master добавляет описание программы в README.md, делает коммит и push. После публикации ветки участника 2 владелец выполняет в СВОЁМ репозитории:

```bash
git checkout master
git pull --ff-only origin master
git fetch origin
git merge --no-ff origin/teammate-help -m "Объединена работа второго участника"
git push origin master
```

Проверьте запуск, заголовок и подпись кнопки. Участник 2 получает результат через `git checkout master` и `git pull --ff-only origin master`. Сохраните историю с авторами: `git log --graph --format="%h %an: %s" --all`. Две локальные ветки одного человека сами по себе не подтверждают работу вдвоём.

## 9. Удаление учебного репозитория

Для этого упражнения создайте ОТДЕЛЬНЫЙ пустой репозиторий lab4-delete-test. Откройте его Settings → General → Danger Zone → Delete this repository и следуйте подтверждениям GitHub. Перед подтверждением проверьте полное имя: удалять нужно только lab4-delete-test. Основной lab4-git-demo сохраните для сдачи. Удаление репозитория на GitHub и удаление папки на компьютере — разные операции.

## 10. Что включить в отчёт

1. Название, цель из методички, ФИО и группа.
2. Среда: Windows, фактическая версия Visual Studio и Git (`git --version`).
3. Описание программы и исходный код.
4. Скриншоты обеих форм; предупреждения при пустом имени; смены фона.
5. Первые коммиты и .gitignore; результат проверки фильтра.
6. Различия веток и история после слияния.
7. Конфликт до и после разрешения.
8. Ссылка на GitHub, файлы, ветки лабораторных и коммиты.
9. Клонирование чужого проекта.
10. Совместная работа с реальным вторым участником и авторами коммитов.
11. Удаление отдельного тестового репозитория.
12. Вывод о реально выполненных действиях и освоенных операциях.

Не вписывайте невыполненные шаги как выполненные. Скриншоты и персональные сведения в исходный архив не включены.

## Источники

Методические указания: приложенный файл «Лабораторная работа № 4.docx».
Microsoft, Git в VS 2019 16.8: https://learn.microsoft.com/en-us/visualstudio/releases/2019/release-notes-v16.8
GitHub, публикация локального кода: https://docs.github.com/en/migrations/importing-source-code/using-the-command-line-to-import-source-code/adding-locally-hosted-code-to-github

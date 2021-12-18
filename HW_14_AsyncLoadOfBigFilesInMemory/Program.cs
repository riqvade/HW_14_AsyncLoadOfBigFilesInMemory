//Занятие 14.Домашнее задание
//Необходимо реализовать консольное приложение загружающее в память большие текстовые
//файлы.
//Разработанное приложение должно предусматривать:
//-индикацию процесса загрузки в виде статичной надписи File Loading… или Загружается
//файл….
//- возможность отмены процесса загрузки по нажатию клавиши (например X).
//-по окончании загрузки выводит на экран результат загрузки (успешно/неуспешно/отменено
//пользователем)
//Пользовательский ввод реализовывать не нужно, путь к исходному файлу можно записать в
//виде константы или использовать путь относительно исполняемого файла приложения.
//Рекомендуется использовать асинхронные методы классов для работы с файлами из
//пространства имён System.IO и классы TaskParallelLibrary.


using HW_14_AsyncLoadOfBigFilesInMemory;
using HW_14_Constants;

try
{
    using StreamReader streamReader = new StreamReader(Constants.PathToLargeFile);
    using CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    CancellationToken cancellationToken = cancellationTokenSource.Token; 

    // Поток необходимый для отмены загрузки файла
    new Task(() => 
    {
        string? canselEnter = string.Empty;
        while (true)
        {
            Console.WriteLine("Для отмены загрузки нажмите \"X\"");
            Console.WriteLine("");

            canselEnter = Console.ReadLine();
            if(canselEnter.Equals("X") || canselEnter.Equals("x"))
            {
                try
                {
                    cancellationTokenSource.Cancel();

                    break;
                }
                catch { }
            }
            else
            {
                Console.WriteLine("Введен некорректный символ!");
            }
        }
    }).Start();

    Console.WriteLine("Загрузка файла");
    await DataReader.ReadDataFromFileWithCanselTokenAsync(streamReader, cancellationToken);
    Console.WriteLine("Файл успешно загружен!");
}
catch (OperationCanceledException ocex)
{
    Console.WriteLine($"Загрузка файла отменена пользователем! {ocex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
    Console.WriteLine("Работа приложения остановлена!");
}
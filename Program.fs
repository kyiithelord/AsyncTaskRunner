namespace AsyncModuleExecutor

open System
open System.IO
open System.Threading.Tasks
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Logging
open AsyncModuleExecutor.Module1
open AsyncModuleExecutor.Module2
open AsyncModuleExecutor.Algorithm

module Program =

    let configureLogging () =
        let loggerFactory = LoggerFactory.Create(fun builder ->
            builder
                .AddConsole()
                .AddDebug()
                .SetMinimumLevel(LogLevel.Information)
        )
        loggerFactory.CreateLogger("AsyncModuleExecutor")

    let configureAppSettings () =
        let configBuilder = ConfigurationBuilder()
        configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional = true, reloadOnChange = true)
                    .Build()

    let parseArgs (args: string[]) =
        if args.Length = 0 then
            printfn "No arguments provided. Running default operations."
            "default"
        else
            args.[0]

    let runModule1Async (logger: ILogger) =
        Task.Run(fun () ->
            try
                logger.LogInformation("Running Module1")
                Module1.run()
            with
            | ex -> logger.LogError("Error in Module1: {0}", ex.Message)
        )

    let runModule2Async (logger: ILogger) =
        Task.Run(fun () ->
            try
                logger.LogInformation("Running Module2")
                Module2.run()
            with
            | ex -> logger.LogError("Error in Module2: {0}", ex.Message)
        )

    let runTSPAsync (logger: ILogger) =
        Task.Run(fun () ->
            try
                logger.LogInformation("Running TSP Algorithm")
                let points = [(0.0, 0.0); (1.0, 1.0); (2.0, 2.0); (3.0, 3.0)]
                let (path, distance) = solveTSP points
                logger.LogInformation("Shortest path: {0}, Distance: {1}", path, distance)
            with
            | ex -> logger.LogError("Error in TSP Algorithm: {0}", ex.Message)
        )

    [<EntryPoint>]
    let main argv =
        let logger = configureLogging()
        let config = configureAppSettings()

        logger.LogInformation("Running complex F# project")

        let operation = parseArgs argv

        match operation with
        | "module1" -> runModule1Async(logger).Wait()
        | "module2" -> runModule2Async(logger).Wait()
        | "tsp" -> runTSPAsync(logger).Wait()
        | _ ->
            logger.LogInformation("Running all modules")
            let task1 = runModule1Async(logger)
            let task2 = runModule2Async(logger)
            let task3 = runTSPAsync(logger)
            Task.WhenAll(task1, task2, task3).Wait()

        0 // return an integer exit code
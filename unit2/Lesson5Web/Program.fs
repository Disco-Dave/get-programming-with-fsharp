open System
open System.Net
open System.Windows.Forms

let getHtml (uri: Uri) =
    use webClient = new WebClient()
    webClient.DownloadString uri

let createBrowser documentText =
    new WebBrowser(ScriptErrorsSuppressed = true,
                   Dock = DockStyle.Fill,
                   DocumentText = documentText)

let createForm title uri  =
    let browser = createBrowser (getHtml uri)
    let form = new Form(Text = title)
    form.Controls.Add browser
    form

[<EntryPoint; STAThread>]
let main _ =
    Application.SetHighDpiMode(HighDpiMode.SystemAware) |> ignore
    Application.EnableVisualStyles()
    Application.SetCompatibleTextRenderingDefault(false)

    use form = createForm "FSharp Org" (Uri "http://fsharp.org")
    Application.Run(form)

    0 // return an integer exit code

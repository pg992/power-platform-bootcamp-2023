import {IInputs, IOutputs} from "./generated/ManifestTypes";

import * as signalR from "@aspnet/signalr"

export class PCFSignalR implements ComponentFramework.StandardControl<IInputs, IOutputs> {

    private _receivedMessage: string;
    private _notifyOutputChanged: () => void;
    private _context: ComponentFramework.Context<IInputs>;
    private connection: signalR.HubConnection;
    private _signalRApi: string;

    /**
     * Empty constructor.
     */
    constructor()
    {

    }

    /**
     * Used to initialize the control instance. Controls can kick off remote server calls and other initialization actions here.
     * Data-set values are not initialized here, use updateView.
     * @param context The entire property bag available to control via Context Object; It contains values as set up by the customizer mapped to property names defined in the manifest, as well as utility functions.
     * @param notifyOutputChanged A callback method to alert the framework that the control has new outputs ready to be retrieved asynchronously.
     * @param state A piece of data that persists in one session for a single user. Can be set at any point in a controls life cycle by calling 'setControlState' in the Mode interface.
     * @param container If a control is marked control-type='standard', it will receive an empty div element within which it can render its content.
     */
    public init(context: ComponentFramework.Context<IInputs>, notifyOutputChanged: () => void, state: ComponentFramework.Dictionary, container:HTMLDivElement): void
    {
        // Add control initialization code
        this._context = context;
        this._notifyOutputChanged = notifyOutputChanged;
        this._signalRApi=context.parameters.SignalRHubConnectionUrl.raw?
        context.parameters.SignalRHubConnectionUrl.raw:"";

        console.log(this._signalRApi)
        console.log('PETARR')

        if(this._signalRApi && this._signalRApi.length > 0) {
            console.log('GO OPEN CONNECTION')
            this.OpenConnection();
        } else {
            console.log("LOG: NO URL");
        }

        
    }

    private OpenConnection() {
        //Create the connection to SignalR Hub
        this.connection = new signalR.HubConnectionBuilder()
        .withUrl(this._signalRApi)
        .configureLogging(signalR.LogLevel.Information) // for debug
        .build();

        console.log("Connection established!")
        //configure the event when a new message arrives
        this.connection.on("newMessage", (message:string) => {
            console.log("NEW MESSAGE: " + message)
            this._receivedMessage=message;
            this._notifyOutputChanged();
        });


        //connect
        this.connection.start()
        .catch(err => {
            console.log(err);
            this.connection.stop();
        });    
    }


    /**
     * Called when any value in the property bag has changed. This includes field values, data-sets, global values such as container height and width, offline status, control metadata values such as label, visible, etc.
     * @param context The entire property bag available to control via Context Object; It contains values as set up by the customizer mapped to names defined in the manifest, as well as utility functions
     */
    public updateView(context: ComponentFramework.Context<IInputs>): void
    {
        //When the MessageToSend is updated this code will run and we send the message to signalR
        this._context = context;
        let messageToSend= JSON.parse(this._context.parameters.MessageToSend.raw!= null?
        this._context.parameters.MessageToSend.raw:"");
        this.httpCall(messageToSend, (res)=>{ console.log(res)});
    }

    /**
     * It is called by the framework prior to a control receiving new data.
     * @returns an object based on nomenclature defined in manifest, expecting object[s] for property marked as “bound” or “output”
     */
    public getOutputs(): IOutputs
    {
         //This code will run when we call notifyOutputChanged when we receive a new message
         //here is where the message gets exposed to outside
        let result: IOutputs = {
            MessageReceivedText: this._receivedMessage,
            MessageReceivedType: this._receivedMessage,
            MessageReceivedSender: this._receivedMessage
        };
        return result;
    }

    /**
     * Called when the control is to be removed from the DOM tree. Controls should use this call for cleanup.
     * i.e. cancelling any pending remote calls, removing listeners, etc.
     */
    public destroy(): void
    {
        // Add code to cleanup control if necessary
        this.connection.stop();
    }

    public httpCall(data:any, callback:(result:any)=>any): void {
        var xhr = new XMLHttpRequest();
        xhr.open("post", this._signalRApi+"/signalr/broadcast", true);
        if (data != null) {
            xhr.setRequestHeader('Content-Type', 'application/json');
            xhr.send(JSON.stringify(data));
        }
        else xhr.send();
    }
}

// class ReceivedModel
// {
//   sender: string;
//   text: string;
//   type:string;
// }
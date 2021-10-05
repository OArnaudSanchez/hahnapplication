import { DialogController } from 'aurelia-dialog';
import { autoinject } from 'aurelia-framework';
 
@autoinject
export class Dialog {    
    title?: string;
    message?: string;
 
    constructor(private dialogController : DialogController) {
        dialogController.settings.centerHorizontalOnly = true;
    }
 
    activate(model : any) {
        this.message = model?.message;
        this.title = model?.title;
     }
 
     ok() : void {
        this.dialogController.ok();
     }

     cancel(): void{
        this.dialogController.cancel();
     }
}
import { inject } from 'aurelia-framework';
import { DialogService } from 'aurelia-dialog';
import { Router } from 'aurelia-router';
import { ValidationRules, ValidationControllerFactory } from 'aurelia-validation';
import { Dialog } from 'components/Dialog';
import { UserService } from '../services/UserService';
import { AssetService } from '../services/AssetService';
import { User } from '../models/User';
import { Asset } from '../models/Asset';
import { BootstrapFormRenderer } from '../common/bootstrap-form-renderer';

@inject(DialogService, Router, ValidationControllerFactory, UserService, AssetService, User, Asset)
export class Index{

    dialogService: DialogService;
    router: Router;
    userService: UserService;
    assetService: AssetService;
    user: User;
    asset: Asset;
    controller = null;
    private assetname: Array<String> = [];

    constructor( dialogService: DialogService, router, controller: ValidationControllerFactory, userService, assetService, user, asset ){
        this.controller = controller.createForCurrentScope();
        this.dialogService = dialogService;
        this.router = router;
        this.controller.addRenderer(new BootstrapFormRenderer());
        this.userService = userService;
        this.assetService = assetService;
        this.user = user;
        this.asset = asset;

        ValidationRules
            .ensure('FirstName').required().minLength(3)
            .ensure('LastName').required().minLength(3)
            .ensure('Address').required().matches(new RegExp('#?(\\d*)\\s?[\\,]?((?:[\\w+\\s*\\-])+)[\\,]\\s?([a-zA-Z]+)\\s?[\\,]?([0-9a-zA-Z]+)-?([0-9]+)'))
            .withMessage('Address EXAMPLE: #8400, NW 25th Street Suite 100, FL 33198-1534')
            .ensure('Email').required().matches(new RegExp('@(.)+(?:[A-Z]{2}|com|org|net|do|gov|biz|info|mobi|name|aero|jobs|museum|tv)\\b'))
            .withMessage('The email must have an @ and a domain such as: .com, .org, .net, .do, .gov, .biz, .info, .mobi, .name, .aero, .jobs, .museum, .tv')
            .ensure('Age').required().min(19)
            .ensure('AssetName').required()
            .on(this.user);
            this.controller.validate();
    }

    async created(){
        this.user.Assets = await this.assetService.GetAssets();
    }

    async submitData(){
        if(this.assetname.length > 0){

            this.user.AssetName = this.assetname;
            this.userService.AddUser(this.user)
            .then(response => {
                if(response?.status === 201){
                    this.router.navigateToRoute('profile', { 'id' : response.data.id });
                }
            })
            .catch(error => {
                if(error){
                    error.response.data.errors.map(error => {
                        this.dialogService.open({
                               viewModel: Dialog, 
                               model: {
                                message: error.message,
                                title: error.title
                               }
                        })
                        .whenClosed().then(response => {
                            if(!response.wasCancelled){
                                this.user = null;
                                this.controller.reset();
                            }
                        });
                    });
                }
            });
        }
    }

    reset(){
        this.dialogService.open({
            viewModel: Dialog,
            model: {
                message: "Really sure to reset all the data?",
                title: "Alert"
            }
        })
        .whenClosed().then(response => {
            if(!response.wasCancelled){
                this.user = null;
                this.controller.reset();
            }
        })
    }

    get isValid(){
        return this.user.FirstName?.length > 0 
        || this.user.LastName?.length > 0 || this.user.Email?.length > 0
        || this.user.Address?.length > 0 || this.user?.Age > 0;
    }
}
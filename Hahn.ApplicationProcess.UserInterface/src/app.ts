import {RouterConfiguration, Router} from 'aurelia-router';
import {PLATFORM} from 'aurelia-pal';

export class App {
  public message: String = 'Hahn Applicaton Process';
  router: Router;

  constructor(){}

  configureRouter(config: RouterConfiguration, router: Router): void{
    this.router = router;
    config.title = "App";
    config.map([
      { route: ['', 'home'], name: 'home', moduleId: PLATFORM.moduleName('form/index'), title: 'Home' },
      { route: 'profile/:id', name: 'profile', moduleId: PLATFORM.moduleName('profile/profile'), title: 'Profile' },
    ]);
  }

}

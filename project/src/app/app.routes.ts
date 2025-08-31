import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { HomeComponent } from './pages/home/home.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { StoryComponent } from './pages/story/story.component';


export const routes: Routes = [
    {path:"",component:LoginComponent},
    {path:"Home",component:HomeComponent},
    { path: "profile", component: ProfileComponent } ,
    { path: "story", component: StoryComponent } ,
    { path: "logout", component: LoginComponent } ,
  
    



];

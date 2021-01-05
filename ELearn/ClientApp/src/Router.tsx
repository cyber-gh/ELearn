import React from 'react';
import Navbar from "./components/Navbar";
import {HashRouter as Router, Route, Switch, Redirect, BrowserRouter} from "react-router-dom";
// import { Router, Route, Switch, Redirect} from "react-router-dom";
import { Container } from 'reactstrap';
import Home from "./pages/HomeView";
import Footer from './components/Footer';
import CourseView from './pages/CourseView';
import {RouteData} from "./interfaces";
import AddCourseView from "./pages/AddCourseView";
import {SnackbarProvider} from "./components/AppSnackBar"
import AddLessonsView from "./pages/AddLessonsView";
import UserCoursesView from "./pages/UserCoursesView";
import {Login} from "./components/api-authorization/Login";
import {ApplicationPaths} from "./components/api-authorization/ApiAuthorizationConstants";
import ApiAuthorizationRoutes from "./components/api-authorization/ApiAuthorizationRoutes";
import AuthorizeRoute from "./components/api-authorization/AuthorizeRoute";

const CustomRoute = ({path, condition, redirect, component: Component}: RouteData) => {
	if (condition) {
		return <Redirect exact from = {path} to = {redirect as string}/>
	}
	return (
		<Route exact path = {path} render = {(props) => <Component {...props}/>}/>
	)
}

const App = () => {
	return(

		<BrowserRouter basename="/">
			<SnackbarProvider>
			{/*<Router>*/}
			<Navbar/>
			<section className="main-window-container">
				<Switch>
					{/*<Redirect exact from = "/" to = "/home"/>*/}
					<Route exact path="/" component={Home}/>
					<Route path="/course/:id" component={CourseView}/>
					<Route path="/add-course" component={AddCourseView}/>
					<Route path="/add-lessons/:id" component={AddLessonsView}/>
					<Route path="/my-classes" component={UserCoursesView}/>
					<Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes}/>
				</Switch>
			</section>
			<Footer/>
			{/*</Router>*/}
			</SnackbarProvider>
		</BrowserRouter>
	)
}

export default App;


// Protected route example
// <AdvancedRoute protect = {true} condition = {true} redirect = "/" path = "/course/:id" component = {CourseView}/> 

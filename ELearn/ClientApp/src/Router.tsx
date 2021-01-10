import history from "./history";
import React, {Component, useContext, useEffect, useState} from 'react';
import Navbar from "./components/Navbar";
import {Router, Route, Switch, Redirect, BrowserRouter} from "react-router-dom";
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
import {ApplicationPaths, QueryParameterNames} from "./components/api-authorization/ApiAuthorizationConstants";
import ApiAuthorizationRoutes from "./components/api-authorization/ApiAuthorizationRoutes";
import AuthorizeRoute from "./components/api-authorization/AuthorizeRoute";
import authService from "./components/api-authorization/AuthorizeService";
import {AuthContext, AuthProvider} from "./components/AuthProvider";
import CoursesCategoryView from "./pages/CoursesCategoryView";

const CustomRoute = ({path, condition, component: Component}: RouteData) => {
	if (!condition) {
		const returnUrl = window.location.href;
		const redirectUrl = `${ApplicationPaths.Login}?${QueryParameterNames.ReturnUrl}=${encodeURIComponent(returnUrl)}`
		// const redirectUrl = `${ApplicationPaths.Login}`
		return <Redirect exact from = {path} to = {redirectUrl}/>
	}
	return (
		<Route exact path = {path} render = {(props) => <Component {...props}/>}/>
	)
}



const App = () => {
	const authData = useContext(AuthContext);
	
	
	return(
		<AuthProvider>
			<Router history={history}>
					<SnackbarProvider>
						{/*<Router>*/}
						<Navbar/>
						<section className="main-window-container">
							<Switch>
								{/*<Redirect exact from = "/" to = "/home"/>*/}
								<Route exact path="/" component={Home}/>
								<CustomRoute path="/courses/:id" condition={authData.authenticated} component={CoursesCategoryView}/>
								<CustomRoute path="/course/:id" condition={authData.authenticated} component={CourseView}/>
								<CustomRoute path="/add-course" condition={authData.authenticated} component={AddCourseView}/>
								<CustomRoute path="/add-lessons/:id" condition={authData.authenticated} component={AddLessonsView}/>
								<CustomRoute path="/my-classes" condition={authData.authenticated} component={UserCoursesView}/>
								<Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes}/>
							</Switch>
						</section>
						<Footer/>
						{/*</Router>*/}
					</SnackbarProvider>
			</Router>
		</AuthProvider>
	)
}

export default App;


// Protected route example
// <AdvancedRoute protect = {true} condition = {true} redirect = "/" path = "/course/:id" component = {CourseView}/> 

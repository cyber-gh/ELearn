import React from 'react';
import Navbar from "./components/Navbar";
import {HashRouter as Router, Route, Switch, Redirect} from "react-router-dom";
import Home from "./pages/HomeView";
import Footer from './components/Footer';
import CourseView from './pages/CourseView';
import {RouteData} from "./interfaces";
import AddCourseView from "./pages/AddCourseView";
import {SnackbarProvider} from "./components/AppSnackBar"
import AddLessonsView from "./pages/AddLessonsView";
import UserCoursesView from "./pages/UserCoursesView";

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
		<SnackbarProvider>
			<Router>
				<Navbar/>
				<section className="main-window-container">
					<Switch>
						<Redirect exact from = "/" to = "/home"/>
						<CustomRoute path = "/home" component = {Home}/>
						<CustomRoute path = "/course/:id" component = {CourseView}/> 
						<CustomRoute path = "/add-course" component={AddCourseView}/>
						<CustomRoute path = "/add-lessons/:id" component={AddLessonsView}/>
						<CustomRoute path = "/my-classes" component={UserCoursesView}/>
					</Switch>
				</section>
				<Footer/>
			</Router>
		</SnackbarProvider>
	)
}

export default App;


// Protected route example
// <AdvancedRoute protect = {true} condition = {true} redirect = "/" path = "/course/:id" component = {CourseView}/> 

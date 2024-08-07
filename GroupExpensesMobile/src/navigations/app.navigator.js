import {createBottomTabNavigator} from 'react-navigation-tabs';

import HomeScreen from '_scenes/home';
import AboutScreen from '_scenes/about';
import EventScreen from '../scenes/event';
import ReportScreen from '../scenes/report';

const TabNavigatorConfig = {
  initialRouteName: 'Home',
  header: null,
  headerMode: 'none',
};

const RouteConfigs = {
  Home: {
    screen: HomeScreen,
  },
  Event: {
    screen: EventScreen,
  },
  Report: {
    screen: ReportScreen,
  },
};

const AppNavigator = createBottomTabNavigator(RouteConfigs, TabNavigatorConfig);

export default AppNavigator;
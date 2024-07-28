import {createAppContainer, createSwitchNavigator} from 'react-navigation';
import AppNavigator from './app-navigator';

const RootNavigator = createSwitchNavigator(
  {
    App: AppNavigator,
  },
  {
    initialRouteName: 'Home',
  },
);

export default createAppContainer(RootNavigator);
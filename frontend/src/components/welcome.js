import React from 'react';
import tw from 'twrnc';
import {Image, SafeAreaView, Text, TouchableOpacity, View} from 'react-native';
import mainLogo from '../../assets/unnamed.png';
import {useNavigation} from '@react-navigation/native';

export const Welcome = () => {
  const navigation = useNavigation();
  return (
    <SafeAreaView style={tw`flex-1 items-center justify-center bg-white`}>
      <View style={tw`flex-row justify-center items-end`}>
        <Text style={tw`text-2xl pb-10 pl-5 font-bold w-50`}>
          Инвестируйте в мировые валюты
        </Text>
        <Image style={tw`w-40 h-50 ml-5`} source={mainLogo} />
      </View>
      <View style={tw`pt-25 items-center`}>
        <TouchableOpacity
          style={tw`bg-[#EB3E1B] w-80 h-10 mb-5 rounded-2`}
          onPress={() => navigation.navigate('SignUpScreen')}>
          <Text style={tw`text-xl text-white m-auto`}>Стать клиентом</Text>
        </TouchableOpacity>
        <TouchableOpacity
          style={tw`bg-[#2E2E41] w-80 h-10 mb-5 rounded-2`}
          onPress={() => navigation.navigate('LoginScreen')}>
          <Text style={tw`text-xl text-white m-auto`}>
            Войти в личный кабинет
          </Text>
        </TouchableOpacity>
      </View>
    </SafeAreaView>
  );
};

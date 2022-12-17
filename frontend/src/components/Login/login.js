import React from 'react';
import tw from 'twrnc';
import {Text, View, TextInput, TouchableOpacity} from 'react-native';

export const Login = () => {
  return (
    <View style={tw`bg-white flex-1`}>
      <View style={tw`m-3 pt-10`}>
        <Text style={tw`font-bold`}>
          <Text style={tw`text-[#FF0000]`}>*</Text>Логин
        </Text>
        <TextInput
          maxLength={20}
          placeholder="Укажите логин"
          style={tw` p-2 h-10 rounded bg-[#F3F3F3]`}
          value={''}
        />
      </View>
      <View style={tw`m-3`}>
        <Text style={tw`font-bold`}>
          <Text style={tw`text-[#FF0000]`}>*</Text>Пароль
        </Text>
        <TextInput
          maxLength={20}
          secureTextEntry={true}
          placeholder="Придумайте пароль"
          textContentType="password"
          style={tw`p-2 h-10 rounded bg-[#F3F3F3]`}
          value={''}
        />
        <TouchableOpacity>
          <Text style={tw`text-[#2E2E41] mt-2`}>Забыли пароль?</Text>
        </TouchableOpacity>
      </View>
      <TouchableOpacity style={tw`bg-[#EB3E1B] w-80 h-10 rounded-2 m-10`}>
        <Text style={tw`text-xl text-white m-auto`}>Продолжить</Text>
      </TouchableOpacity>
    </View>
  );
};

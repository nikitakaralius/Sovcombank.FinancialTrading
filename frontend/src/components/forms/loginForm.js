import React, {useState} from 'react';
import {useFormik} from 'formik';
import {useNavigation} from '@react-navigation/native';
import * as yup from 'yup';
import tw from 'twrnc';
import {Text, TextInput, TouchableOpacity, View} from 'react-native';

export const LoginForm = () => {
  const navigation = useNavigation();
  const [loginError, setLoginError] = useState(null);
  const initialValues = {
    email: '',
    password: '',
  };

  const loginSchema = yup.object().shape({
    email: yup
      .string()
      .trim()
      .email('Пожалуйста введите корректную почту')
      .required('Необходимо заполнить'),
    password: yup.string().trim().required('Необходимо заполнить'),
  });

  const {handleSubmit, handleChange, values, errors, touched} = useFormik({
    initialValues: initialValues,
    validationSchema: () => {
      setLoginError(null);

      return loginSchema;
    },
    onSubmit: values => {
      console.log(`Email: ${values.email}, Password: ${values.password}`);
      navigation.goBack();
    },
  });

  return (
    <View style={tw`bg-white flex-1`}>
      <View style={tw`m-3 pt-10`}>
        <Text style={tw`font-bold`}>
          <Text style={tw`text-[#FF0000]`}>*</Text>Почта
        </Text>
        <TextInput
          maxLength={20}
          placeholder="Укажите почту"
          style={tw` p-2 h-10 rounded bg-[#F3F3F3]`}
          onChangeText={handleChange('email')}
          error={errors.email}
          value={values.email}
        />
        {errors.email && touched.email && (
          <Text style={tw`text-[#FF0000] mx-1`}>{errors.email}</Text>
        )}
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
          onChangeText={handleChange('password')}
          error={errors.password}
          value={values.password}
        />
        {errors.password && touched.password && (
          <Text style={tw`text-[#FF0000] mx-1`}>{errors.password}</Text>
        )}
        <TouchableOpacity>
          <Text style={tw`text-[#2E2E41] mt-2`}>Забыли пароль?</Text>
        </TouchableOpacity>
      </View>
      <TouchableOpacity
        style={tw`bg-[#EB3E1B] w-80 h-10 rounded-2 m-10`}
        onPress={handleSubmit}>
        <Text style={tw`text-xl text-white m-auto`}>Продолжить</Text>
      </TouchableOpacity>
    </View>
  );
};

import React, {useState} from 'react';
import {useFormik} from 'formik';
import {useNavigation} from '@react-navigation/native';
import * as yup from 'yup';
import tw from 'twrnc';
import {Text, TextInput, TouchableOpacity, View} from 'react-native';

export const SignUpForm = () => {
  const navigation = useNavigation();
  const [signUpError, setSignUpError] = useState(null);
  const initialValues = {
    email: '',
    tel: '',
    password: '',
    confirmPassword: '',
  };

  const signUpSchema = yup.object().shape({
    email: yup
      .string()
      .trim()
      .email('Пожалуйста введите корректную почту')
      .required('Необходимо заполнить'),
    tel: yup.string().trim().required('Необходимо заполнить'),
    password: yup.string().trim().min(6, 'Пароль слишком короткий').required('Необходимо заполнить'),
    confirmPassword: yup
      .string()
      .oneOf([yup.ref('password')], 'Пароли не совпадают'),
  });

  const {handleSubmit, handleChange, values, errors, touched} = useFormik({
    initialValues: initialValues,
    validationSchema: () => {
      setSignUpError(null);

      return signUpSchema;
    },
    onSubmit: values => {
      console.log(`Email: ${values.email}, Password: ${values.password}`);
      navigation.goBack();
    },
  });

  return (
    <View style={tw`bg-white flex-1`}>
      <View style={tw`m-5`}>
        <Text style={tw`font-bold`}>
          <Text style={tw`text-[#FF0000]`}>*</Text>Почта
        </Text>
        <TextInput
          placeholder="Укажите почту"
          textContentType="emailAddress"
          style={tw`p-2 h-10 rounded bg-[#F3F3F3]`}
          onChangeText={handleChange('email')}
          error={errors.email}
          value={values.email}
        />
        {errors.email && touched.email && (
          <Text style={tw`text-[#FF0000] mx-1`}>{errors.email}</Text>
        )}
      </View>
      <View style={tw`m-5`}>
        <Text style={tw`font-bold`}>
          <Text style={tw`text-[#FF0000]`}>*</Text>Номер телефона
        </Text>
        <TextInput
          keyboardType="phone-pad"
          placeholder="Укажите номер телефона"
          onChangeText={handleChange('tel')}
          textContentType="telephoneNumber"
          style={tw`p-2 h-10 rounded bg-[#F3F3F3]`}
          error={errors.tel}
          value={values.tel}
        />
        {errors.tel && touched.tel && (
          <Text style={tw`text-[#FF0000] mx-1`}>{errors.tel}</Text>
        )}
      </View>
      <View style={tw`m-5`}>
        <Text style={tw`font-bold`}>
          <Text style={tw`text-[#FF0000]`}>*</Text>Пароль
        </Text>
        <TextInput
          maxLength={20}
          secureTextEntry={true}
          placeholder="Придумайте пароль"
          textContentType="password"
          onChangeText={handleChange('password')}
          style={tw`p-2 h-10 rounded bg-[#F3F3F3]`}
          error={errors.password}
          value={values.password}
        />
        {errors.password && touched.password && (
          <Text style={tw`text-[#FF0000] mx-1`}>{errors.password}</Text>
        )}
      </View>
      <View style={tw`m-5`}>
        <Text style={tw`font-bold`}>
          <Text style={tw`text-[#FF0000]`}>*</Text>Подтвердите пароль
        </Text>
        <TextInput
          maxLength={20}
          secureTextEntry={true}
          placeholder="Подтвердите пароль"
          textContentType="password"
          onChangeText={handleChange('confirmPassword')}
          style={tw`p-2 h-10 rounded bg-[#F3F3F3]`}
          errors={errors.confirmPassword}
          value={values.confirmPassword}
        />
        {errors.confirmPassword && touched.confirmPassword && (
          <Text style={tw`text-[#FF0000] mx-1`}>{errors.confirmPassword}</Text>
        )}
      </View>
      <TouchableOpacity
        style={tw`bg-[#EB3E1B] w-80 h-10 rounded-2 m-10`}
        onPress={handleSubmit}>
        <Text style={tw`text-xl text-white m-auto`}>Стать клиентом</Text>
      </TouchableOpacity>
    </View>
  );
};

/*import React from 'react';
import { shallow } from 'enzyme';
import useReserveCar from "./ReserveCar.utils";

function HookWrapper(props) {
  const hook = props.hook ? props.hook() : undefined;
  return <div hook={hook} />;
}

describe('useReserveCar', () => {
  it('should render', () => {
    let wrapper = shallow(<HookWrapper />);

    expect(wrapper.exists()).toBeTruthy();
  });

  it('should set init value', () => {
    let wrapper = shallow(<HookWrapper hook={() => useReserveCar()} />);

    let { hook } = wrapper.find('div').props();
    let {CompareString} = hook;
    expect(CompareString("abc","ABC")).toBeTruthy();
  });

});
*/
import useReserveCar from "./ReserveCar.utils";
import { renderHook, act } from '@testing-library/react-hooks'

it('Should have initial value of 0', () => {
    const {CompareString}  = renderHook(() => useReserveCar());
    const result=CompareString("abc","ABC")
    expect(result).toBeTruthy();
  });
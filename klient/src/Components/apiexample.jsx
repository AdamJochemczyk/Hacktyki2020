const useCreateCar = () => {
    const [isLoading, setIsLoading] = useState(false)
    const [error, setError] = useState('')
  
  const createCar = async (values) =>{ 
    try {
        setIsLoading(true)
      const reponse = await api.createCar(values)
        setIsLoading(false)
      return reponse.data
    } catch(err){   
        setIsLoading(false)
      setError(err)
    }
  }
    return { isLoading, error, createCar }
}

const useGetCar = () => {
  const [isLoading, setIsLoading] = useState(false)
  const [error, setError] = useState(null)
  const [car, setCar] = useState(null)
  
  const getCar = async (id) =>{ 
    try {
      setIsLoading(true)
      const reponse = await api.createCar(id)
      setIsLoading(false)
      setCar(response.data)
    } catch(err){   
      setIsLoading(false)
      setError(err)
    }
  }
  return { isLoading, error, car, getCar }
}

const useGetCar = () => {
  const [isLoading, setIsLoading] = useState(false)
  const [error, setError] = useState(null)
  const [car, setCar] = useState(null)
  
  const getCar = async (id) =>{ 
    try {
      setIsLoading(true)
      const reponse = await api.createCar(id)
      setIsLoading(false)
      setCar(response.data)
    } catch(err){   
      setIsLoading(false)
      setError(err)
    }
  }
  return { isLoading, error, getCar }
}
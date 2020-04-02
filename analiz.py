#!/usr/bin/env python
# coding: utf-8

# In[8]:


import numpy as np


# In[9]:


import pandas as pd


# In[10]:


from tensorflow.python.keras.models import Sequential
from tensorflow.python.keras.layers import Dense, GRU, Embedding, CuDNNGRU
from tensorflow.python.keras.optimizers import Adam
from tensorflow.python.keras.preprocessing.text import Tokenizer
from tensorflow.python.keras.preprocessing.sequence import pad_sequences


# In[11]:


veriseti = pd.read_csv('dataset.csv')


# In[12]:


veriseti


# In[13]:


etiket = veriseti['Rating'].values.tolist()
yorum = veriseti['Review'].values.tolist()


# Capraz Dogrulama %80 = Egitim Seti - %20 = Test Seti

# In[14]:


kesim_noktasi = int(len(yorum) * 0.80)
yorum_egitim, yorum_test = yorum[:kesim_noktasi], yorum[kesim_noktasi:]
etiket_egitim, etiket_test = etiket[:kesim_noktasi], etiket[kesim_noktasi:]


# In[15]:


yorum_egitim[1997]


# In[16]:


etiket_egitim[1997]


# Tokenlestirme

# In[17]:


max_kelime = 10000
tokenizer = Tokenizer(num_words = max_kelime)


# In[18]:


tokenizer.fit_on_texts(yorum)


# In[19]:


tokenizer.word_index


# In[20]:


yorum_egitim_tokenler = tokenizer.texts_to_sequences(yorum_egitim)


# In[21]:


yorum_egitim[1997]


# In[22]:


yorum_egitim_tokenler[1997]


# In[23]:


yorum_test_tokenler = tokenizer.texts_to_sequences(yorum_test)


# In[24]:


toplam_tokenler = [len(tokenler) for tokenler in yorum_egitim_tokenler + yorum_test_tokenler]
toplam_tokenler = np.array(toplam_tokenler)


# In[25]:


np.mean(toplam_tokenler)


# In[26]:


np.max(toplam_tokenler)


# In[27]:


np.argmax(toplam_tokenler)


# In[28]:


yorum_egitim[21941]


# Padding

# In[29]:


max_token = np.mean(toplam_tokenler) + 2 * np.std(toplam_tokenler)
max_token = int(max_token)
max_token


# In[30]:


np.sum(toplam_tokenler < max_token) / len(toplam_tokenler)


# In[31]:


yorum_egitim_pad = pad_sequences(yorum_egitim_tokenler, maxlen = max_token)


# In[32]:


yorum_test_pad = pad_sequences(yorum_test_tokenler, maxlen = max_token)


# In[33]:


yorum_egitim_pad.shape


# In[34]:


yorum_test_pad.shape


# In[35]:


(yorum_egitim[1997])


# In[36]:


np.array(yorum_egitim_tokenler[1997])


# In[37]:


yorum_egitim_pad[1997]


# In[38]:


yorum_id = tokenizer.word_index
ters_map = dict(zip(yorum_id.values(), yorum_id.keys()))


# In[39]:


def token_tostr(tokenler):
    kelimeler = [ters_map[token] for token in tokenler if token!=0]
    str = ' '.join(kelimeler)
    return str


# In[40]:


yorum_egitim[1997]


# In[41]:


token_tostr(yorum_egitim_tokenler[1997])


# Sinir agı modelleme

# In[42]:


model = Sequential()


# In[43]:


embedding_boyut = 50


# In[44]:


model.add(Embedding(input_dim = max_kelime, output_dim = embedding_boyut, input_length = max_token, name = 'embedding_katman'))


# In[45]:


model.add(CuDNNGRU(units = 16, return_sequences = True))
model.add(CuDNNGRU(units = 8, return_sequences = True))
model.add(CuDNNGRU(units = 4, return_sequences = False))
model.add(Dense(1, activation ='sigmoid'))


# In[46]:


optimizer = Adam(lr=1e-3)


# In[47]:


model.compile(loss='binary_crossentropy',optimizer=optimizer,metrics=['accuracy'])


# In[48]:


model.summary()


# In[49]:


model.fit(yorum_egitim_pad, etiket_egitim, epochs=5, batch_size=256)


# In[50]:


sonuc = model.evaluate(yorum_test_pad, etiket_test)


# In[51]:


sonuc[1]


# Test

# In[52]:


etiket_pred = model.predict(x=yorum_test_pad[0:1000])
etiket_pred = etiket_pred.T[0]


# In[53]:


tahmin_pred = np.array([1.0 if p>0.5 else 0.0 for p in etiket_pred])


# In[54]:


tahmin_true = np.array(etiket_test[0:1000])


# In[55]:


tahmin_incorrect = np.where(tahmin_pred != tahmin_true)
tahmin_incorrect = tahmin_incorrect[0]


# In[56]:


len(tahmin_incorrect)


# In[57]:


metin1 = "Başarılı bir ürün"
metin2 = "Hiç beğenmedim tavsiye etmem"
metin3 = "Fena değil"
metin4 = "Şüphe etmeden alabilirsiniz"
metin5 = "Kesinlikle berbat bir ürün"
metin6 = "Başlangıçta emin değildim ama kesinlikle iyi"
metin7 = "Almayın para israfı. Resmen rezalet"
metin8 = "Başta emin değildim ama kullandıktan sonra bütün fikirlerim değişti"
metinler = [metin1,metin2,metin3,metin4,metin5,metin6,metin7,metin8]


# In[58]:


tokenler = tokenizer.texts_to_sequences(metinler)


# In[59]:


tokenler_pad = pad_sequences(tokenler, maxlen=max_token)
tokenler_pad.shape


# In[60]:


model.predict(tokenler_pad)


# In[ ]:




